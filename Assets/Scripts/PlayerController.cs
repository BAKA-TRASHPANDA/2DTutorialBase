using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Movement_variables
    public float movementSpeed;
    float X_input;
    float Y_input;
    #endregion

    #region Health_variables
    public float maxHP;
    float currentHP;
    public Slider HPslider;
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    #endregion

    #region Attack_var
    public float Attack_speed = 1;
    public float damage;
    float Attack_timer;
    public float hitboxtiming;
    public float endanimationtiming;
    bool is_Attacking;
    Vector2 Current_direction;
    #endregion

    #region Attack_functions
    private void Attack()
    {
        Debug.Log("hit");
        Debug.Log(Current_direction);
        StartCoroutine(AttackRoutine());
        Attack_timer = Attack_speed;
    }
    IEnumerator AttackRoutine()
    {
        is_Attacking = true;
        PlayerRB.velocity = Vector2.zero;
        anim.SetTrigger("attackTrig");

        FindObjectOfType<AudioManager>().Play("PlayerAttack");
        yield return new WaitForSeconds(hitboxtiming);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + Current_direction, Vector2.one,0f, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Enemy>().takeDamage(damage);
            }
        }
        yield return new WaitForSeconds(hitboxtiming);
        is_Attacking = false;
        yield return null;
    }
    #endregion

    #region Unity_functions
    private void Awake()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        Attack_timer = 0;
        anim = GetComponent<Animator>();
        currentHP = maxHP;

        HPslider.value = currentHP / maxHP;
    }
    private void Update()
    {
        if (is_Attacking)
        {
            return;
        }

        X_input = Input.GetAxisRaw("Horizontal");
        Y_input = Input.GetAxisRaw("Vertical");
        Move();
        if (Input.GetKeyDown(KeyCode.J) && Attack_timer <= 0)
        {
            Attack();
        }
        else
        {
            Attack_timer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.L)){
            Interact();
        }
    }
    #endregion

    #region Movement_functions
    private void Move()
    {
        anim.SetBool("isMoving", true);
       if(X_input > 0)
        {
            PlayerRB.velocity = Vector2.right * movementSpeed; 
            Current_direction = Vector2.right;
        }
        else if (X_input < 0)
        {
            PlayerRB.velocity = Vector2.left * movementSpeed; 
            Current_direction = Vector2.left;
        }
        else if (Y_input > 0)
        {
            PlayerRB.velocity = Vector2.up * movementSpeed; 
            Current_direction = Vector2.up;
        }
        else if (Y_input < 0)
        {
            PlayerRB.velocity = Vector2.down * movementSpeed; 
            Current_direction = Vector2.down;
        }
        else
        {
            PlayerRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
        anim.SetFloat("DirX", Current_direction.x);
        anim.SetFloat("DirY", Current_direction.y);

    }
    #endregion
    #region Animation_components
    Animator anim;
    #endregion

    #region Health_functions
    public void takeDamage(float val) {
        FindObjectOfType<AudioManager>().Play("PlayerHurt");
        HPslider.value = currentHP / maxHP;
        currentHP -= val;
        if(currentHP <= 0)
        {
            Die();
        }
    }
    public void Heal(float val)
    {
        currentHP += val;
        currentHP = Mathf.Min(currentHP, maxHP);
    }

    private void Die()
    {
        GameObject gm = GameObject.FindWithTag("GameController");
        gm.GetComponent<GameManager>().WinGame();
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        Destroy(this.gameObject);
    }
    #endregion

    #region Interact_functions
    private void Interact()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position+ Current_direction, new Vector2(0.5f, 0.5f), 0f, Vector2.zero, 0f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Chest"))
            {
                hit.transform.GetComponent<Chest>().interact();
            }
        }
    }
    #endregion
}
