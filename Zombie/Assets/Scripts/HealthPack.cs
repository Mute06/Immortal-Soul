using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

public class HealthPack : MonoBehaviour
{
    public float HP = 40f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().Heal(HP,true);
            AudioManager.instance.PlaySound("Heal");
            Destroy(gameObject);
        }
    }
}
