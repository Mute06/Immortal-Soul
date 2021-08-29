using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent OnEnter; 
    [SerializeField] public GameObject objectToEnable;
    public bool isDelayed;
    public float delayTime;
    bool didWorked;
    private void OnTriggerEnter(Collider other)
    {
        if (!didWorked && other.CompareTag("Player") )
        {
            
            objectToEnable.SetActive(true);
            didWorked = true;
            if (isDelayed)
            {
                if (OnEnter != null)
                {
                    StartCoroutine(Delayed());
                }
            }
            else
            {
                if (OnEnter != null)
                {
                    OnEnter.Invoke();
                }
            }

        }
    }
    private IEnumerator Delayed()
    {
        yield return new WaitForSeconds(delayTime);
        OnEnter.Invoke();
    }

}
