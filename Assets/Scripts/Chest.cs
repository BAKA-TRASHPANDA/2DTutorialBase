using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    #region GameObject_variables
    [SerializeField]
    private GameObject HealthPack;
    #endregion

    #region Chest_func
    IEnumerator DestroyChest()
    {
        yield return new WaitForSeconds(.3f);

        Instantiate(HealthPack, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    public void interact()
    {
        StartCoroutine("DestroyChest");
    }
    #endregion
}
