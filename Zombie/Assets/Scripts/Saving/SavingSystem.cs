using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public IEnumerator LoadLastScene(string savfile)
        {
            Dictionary<string, object> State = LoadFile(savfile);
            int buildIndex = SceneManager.GetActiveScene().buildIndex;

            if (State.ContainsKey("LastSceneBuildIndex"))
            {
                buildIndex = (int)State["LastSceneBuildIndex"];
            }
            yield return SceneManager.LoadSceneAsync(buildIndex);
            RestoreState(State);
        }

        public void Save(string savfile)
        {
            Dictionary<string, object> state = LoadFile(savfile);
            CaptureState(state);
            
            SaveFile(savfile,state);
        }       

        public void Load(string savfile)
        {           
            RestoreState(LoadFile(savfile));
        }

        public void Delete(string savfile)
        {
            string path = GetSavepath(savfile);
            print("Deleted file:" + path);
            File.Delete(path);
        }

        private void SaveFile(string savfile, object state)
        {
            string path = GetSavepath(savfile);
            print("Save to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private Dictionary<string, object> LoadFile(string savfile)
        {
            string path = GetSavepath(savfile);
            if (!File.Exists(path)) { return new Dictionary<string, object>() ; }
            
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
             
        }

        private void CaptureState(Dictionary<string, object> state)
        {            
            foreach (SavableEntity savable in FindObjectsOfType<SavableEntity>())
            {
                state[savable.GetUniqueIdentifier()] = savable.CaptureState();
            }

            state["LastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(Dictionary<string, object> state)
        {            
            foreach (SavableEntity savable in FindObjectsOfType<SavableEntity>())
            {
                string id = savable.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    savable.RestoreState(state[id]);
                }                
            }
        }

        private string GetSavepath(string savfile)
        {
            return Path.Combine(Application.persistentDataPath,savfile+".sav");
        }
    }
}
