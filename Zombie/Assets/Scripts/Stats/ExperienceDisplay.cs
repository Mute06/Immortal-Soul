using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        TextMeshProUGUI textMeshProUGUI;
        // Start is called before the first frame update
        void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {             
            textMeshProUGUI.SetText(string.Format("{0}", experience.GetExperience()));
        }
    }

}