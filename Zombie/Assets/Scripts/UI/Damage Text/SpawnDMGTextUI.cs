using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class SpawnDMGTextUI : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab;


        public void Spawn(float damageAmount)
        {
            DamageText Instance = Instantiate<DamageText>(damageTextPrefab, transform);
            Instance.SetValue(damageAmount);

        }


    }
}
