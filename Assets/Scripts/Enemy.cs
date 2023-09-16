using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Movement_var
    public float movespeed;
    #endregion

    #region Health_variables
    public float maxHP;
    float currentHP;
    #endregion


    #region Attack_var
    public float damage;
    public float radius;
    public GameObject explosion;
    #endregion

    #region Physics_Component
    Rigidbody2D EnemyRB;
    #endregion

    #region Target_var
    public Transform player;
    #endregion

    #region Unity_function
    private void Awake()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }

    private void Update()
    {
        if(player == null)
        {
            return;
        }
        Move();
    }

    #endregion

    #region Movement_functions
    private void Move()
    {
        Vector2 direction = player.position - transform.position;
        EnemyRB.velocity = direction.normalized * movespeed;
    }
    #endregion

    #region Attack_functions
    private void explode()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero);
        FindObjectOfType<AudioManager>().Play("Explode");
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                Instantiate(explosion, transform.position, transform.rotation);
                hit.transform.GetComponent<PlayerController>().takeDamage(damage);

            }
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            explode();
        }
    }
    #endregion

    #region Health_functions
    public void takeDamage(float val)
    {
        FindObjectOfType<AudioManager>().Play("BatHurt");
        currentHP -= val;
        if (currentHP <= 0)
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
        Destroy(this.gameObject);
    }
    #endregion

}
