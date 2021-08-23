using RPG.Attributes;
using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour//,IRaycastable
    {
        [SerializeField] WeaponConfig weapon;
        [SerializeField] float healthTORestore;
        private void OnTriggerEnter(Collider other)
        {           
            if (other.CompareTag("Player"))
            {
                Pickup(other.gameObject);
            }
        }

        private void Pickup(GameObject subject)
        {
            if(weapon!=null)
            {
                //subject.GetComponent<Fighter>().EquipWeapon(weapon);
            }
            
            if(healthTORestore>0)
            {
                subject.GetComponent<Health>().Heal(healthTORestore);
            }
            StartCoroutine(HideForSeconds(3));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            HidePickup();
            yield return new WaitForSeconds(seconds);
            ShowPickup();
        }

        private void ShowPickup()
        {
            GetComponent<Collider>().enabled = true;
            foreach (Transform pickup in transform)
            {
                pickup.gameObject.SetActive(true);
            }
        }

        private void HidePickup()
        {
            GetComponent<Collider>().enabled = false;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        public bool HandleRaycast(PlayerController playerController)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Pickup(playerController.gameObject);
            }
            return true;
        }

        public CursorType GetCursourType()
        {
            return CursorType.Pickup;
        }
    }
}
