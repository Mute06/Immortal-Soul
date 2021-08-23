using RPG.Attributes;
using System;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        TextMeshProUGUI textMeshProUGUI;
        // Start is called before the first frame update
        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if(fighter.GetTarget() == null)
            {                
                textMeshProUGUI.SetText(string.Format("{0:0}", "N/A"));
                return;
            }
            Health health = fighter.GetTarget();
            textMeshProUGUI.SetText(string.Format("{0:0}%", health.GetPercentageHealth()));            
        }
    }
}
