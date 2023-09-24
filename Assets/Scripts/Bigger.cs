using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigger : MonoBehaviour
{
    #region Bigger_var
    [SerializeField]
    #endregion

    #region sped_func
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().movementSpeed += 1;
            collision.transform.GetComponent<PlayerController>().Attack_speed -= .1f;
            Destroy(this.gameObject);
        }
    }
    #endregion
}
