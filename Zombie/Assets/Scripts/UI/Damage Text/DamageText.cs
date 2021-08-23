using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textMeshProUGUI;
        private void Start()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }


        public void SetValue(float damage)
        {
            textMeshProUGUI.SetText(string.Format("{0:0}",damage));
        }

        public void DestroyText()
        {
            Destroy(gameObject);
        }
    }
}
