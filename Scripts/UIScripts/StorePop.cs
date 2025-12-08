using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePop : MonoBehaviour
{

    public bool StorePlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StorePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StorePlayer = false;
        }
    }
}
