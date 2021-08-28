using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingNote : MonoBehaviour
{
    public GameObject noteImage;

    private bool isEnabled = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !isEnabled)
        {
            noteImage.SetActive(true);
            isEnabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isEnabled)
        {
            noteImage.SetActive(false);
            isEnabled = false;
        }
    }
}
