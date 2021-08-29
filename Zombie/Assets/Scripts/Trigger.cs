using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] public GameObject objectToEnable;
    bool didWorked;
    private void OnTriggerEnter(Collider other)
    {
        if (!didWorked && other.CompareTag("Player") )
        {
            objectToEnable.SetActive(true);
            didWorked = true;
        }
    }
}
