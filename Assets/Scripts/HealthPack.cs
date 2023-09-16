using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    #region HealthPack_var
    [SerializeField]
    private int healAmount;
    #endregion

    #region Heal_func
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().Heal(healAmount);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
