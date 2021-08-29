using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTrigger : MonoBehaviour
{
    private bool didWorked;
    private void OnTriggerEnter(Collider other)
    {
        if (!didWorked && other.CompareTag("Player"))
        {
            LevelLoader.instance.LoadScene(LevelLoader.instance.GetActiveSceneIndex());
            didWorked = true;
        }
    }
}
