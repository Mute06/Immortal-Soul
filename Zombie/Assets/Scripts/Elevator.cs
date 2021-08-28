using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool didWorked;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!didWorked)
            {
                LevelLoader.instance.LoadNextScene();
                didWorked = true;
            }
        }
    }
}
