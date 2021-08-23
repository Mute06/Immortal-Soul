using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SavableEntity : MonoBehaviour
    {
        [SerializeField] string UniqueIdentifier = "";
        static Dictionary<string, SavableEntity> GlobalLookup = new Dictionary<string, SavableEntity>();
        public string GetUniqueIdentifier()
        {
            return UniqueIdentifier;
        }

        public object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();

            foreach (ISavable savable in GetComponents<ISavable>())
            {
                state[savable.GetType().ToString()] = savable.CaptureState();
            }

            return state;
            
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDict =(Dictionary<string, object>)state;

            foreach (ISavable savable in GetComponents<ISavable>())
            {
                string typeString = savable.GetType().ToString();
                if (stateDict.ContainsKey(typeString))
                {
                    savable.RestoreState(stateDict[typeString]);
                }              
            }  
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) { return; }
            if (string.IsNullOrEmpty(gameObject.scene.path)) { return; }
            
            SerializedObject serializedObject= new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("UniqueIdentifier");

            if (string.IsNullOrEmpty(serializedProperty.stringValue) || !IsUnique(serializedProperty.stringValue))
            {
                serializedProperty.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            GlobalLookup[serializedProperty.stringValue] = this;
        }
#endif
        private bool IsUnique(string candidate)
        {
            if (!GlobalLookup.ContainsKey(candidate)) { return true; }

            if (GlobalLookup[candidate] == this) { return true; }

            if (GlobalLookup[candidate] == null) 
            { 
                GlobalLookup.Remove(candidate);
                return true;
            }

            if(GlobalLookup[candidate].GetUniqueIdentifier()!=candidate)
            {
                GlobalLookup.Remove(candidate);
                return true;
            }

            return false;
        }

    }
}
