using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour,ISavable
    {
        [SerializeField] float ExperincePoints = 0;

        public event Action OnExperienceGained;
        public void GainExperience(float experience)
        {
            ExperincePoints += experience;
            OnExperienceGained();
        }

        public float GetExperience()
        {
            return ExperincePoints;
        }

        public object CaptureState()
        {
            return ExperincePoints;
        }

       

        public void RestoreState(object state)
        {
            ExperincePoints=(float)state;
        }
    }
}
