using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        TextMeshProUGUI textMeshProUGUI;
        // Start is called before the first frame update
        void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            textMeshProUGUI.SetText(string.Format("{0}", baseStats.CalculateExperienceLevel()));
        }
    }
}
