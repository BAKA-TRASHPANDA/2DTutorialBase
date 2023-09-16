using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSIght : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<Enemy>().player = other.transform;
        }
    }
}
