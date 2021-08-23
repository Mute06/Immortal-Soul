using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        TextMeshProUGUI textMeshProUGUI;
        // Start is called before the first frame update
        void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {            
            textMeshProUGUI.SetText(string.Format("{0:0}/{1:0}", health.GetHealtPoints(), health.GetMAXHealtPoints()));
        }
    }
}
