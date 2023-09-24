using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    #region GameObject_variables
    [SerializeField]
    private GameObject HealthPack;
    [SerializeField]
    private GameObject Bigger;
    private int num = 0;
    #endregion

    #region Chest_func
    IEnumerator DestroyChest()
    {
        yield return new WaitForSeconds(.3f);
        
        if(num <5){
            Instantiate(HealthPack, transform.position, transform.rotation);
        }
        else{
            Instantiate(Bigger, transform.position, transform.rotation);
        }
        
        

        Destroy(this.gameObject);
    }

    public void interact()
    {
        var rnd = new System.Random();
        num = rnd.Next(0,10);
        StartCoroutine("DestroyChest");
    }
    #endregion
}
