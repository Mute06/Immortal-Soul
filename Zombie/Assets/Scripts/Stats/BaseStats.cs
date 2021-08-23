using GameDevTV.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int StartingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progressionType;
        [SerializeField] GameObject LevelUPFX;
        [SerializeField] bool shouldUseModifier = false;
        Experience experience;
        LazyValue<int> currentLevel;

        public event Action OnLevelUp;
        private void Awake()
        {
            experience = GetComponent<Experience>();
            currentLevel = new LazyValue<int>(CalculateExperienceLevel);
        }

        private void Start()
        {
            currentLevel.ForceInit();
        }

        private void OnEnable()
        {
            if (experience != null)
            {
                experience.OnExperienceGained += UpdateLevel;
            }
        }

        private void OnDisable()
        {
            if (experience != null)
            {
                experience.OnExperienceGained -= UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int NewLevel = CalculateExperienceLevel();
            if (NewLevel> currentLevel.value)
            {
                currentLevel.value = NewLevel;
                LevelUPEffect();
                OnLevelUp();
            }
        }

        private void LevelUPEffect()
        {
            Instantiate(LevelUPFX,transform);
        }

        public float GetStat(Stats stat)
        {
            return (progressionType.GetStat(stat, characterClass, CalculateExperienceLevel()) + GetAdditiveModifier(stat)) * (1 + (GetPercentageModifier(stat)/100));
        }

        private float GetPercentageModifier(Stats stat)
        {
            if (!shouldUseModifier) { return 0; }
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifier(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetAdditiveModifier(Stats stats)
        {
            if (!shouldUseModifier) { return 0; }
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stats))
                {
                    total += modifier;
                }
            }
            return total;
        }

        public int CalculateExperienceLevel()
        {
            Experience experience = GetComponent<Experience>(); 
            if (experience == null) { return StartingLevel; }
            float currentXP = experience.GetExperience();

            int PenultimateLevel = progressionType.GetLevels(Stats.ExperienceToLevelUP, characterClass);

            for (int level = 1; level <= PenultimateLevel; level++)
            {
                float XPtoLEvelUp = progressionType.GetStat(Stats.ExperienceToLevelUP, characterClass, level);
                if(currentXP < XPtoLEvelUp)
                {
                    return level;
                }
            }

            return PenultimateLevel + 1;
        }


        public int GetLEvel()
        {

            if(currentLevel.value < 1 )
            {
                currentLevel.value = CalculateExperienceLevel();
            }
            return currentLevel.value;
        }

    }
}
