using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour , IDoor
{
    private Animator animator;
    private bool isOpen;
    private int boolID;
    public Key key;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boolID = Animator.StringToHash("isOpen");
    }

    public void CloseDoor()
    {
        animator.SetBool(boolID, false);
        isOpen = false;
    }

    public void OpenDoor()
    {
        animator.SetBool(boolID, true);
        isOpen = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            if (key != null)
            {
                if (KeyManager.instance.DoIHaveThisKey(key))
                {
                    OpenDoor();
                }
                else
                {
                    // You need key message
                }
            }
            else
            {
                OpenDoor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            CloseDoor();
        }
    }


}
