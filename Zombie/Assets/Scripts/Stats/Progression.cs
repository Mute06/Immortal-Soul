using System;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Stats
{

    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progress", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses;
        Dictionary<CharacterClass, Dictionary<Stats, float[]>> lookupTable;

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
            //
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stats stats;
            public float[] Levels;            
        }



        public int GetLevels(Stats stats,CharacterClass characterClass)
        {
            BuildLookUp();

            float[] levels = lookupTable[characterClass][stats];
            return levels.Length;
        }

        internal float GetStat(Stats stats,CharacterClass characterClass,int level)
        {
            BuildLookUp();
            float[] levels = lookupTable[characterClass][stats];
            if (levels.Length < level) { return 0; }
            return levels[level-1];
        }

        private void BuildLookUp()
        {
            if (lookupTable != null) { return; }

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stats, float[]>>();
            foreach (ProgressionCharacterClass progressCharacter in characterClasses)
            {
                var statLookup = new Dictionary<Stats, float[]>();

                foreach (ProgressionStat progressstat in progressCharacter.stats)
                {
                    statLookup[progressstat.stats] = progressstat.Levels;
                }

                lookupTable[progressCharacter.characterClass] = statLookup;
            }
        }
    }
}
