using RPG.Saving;
using System;
using System.Collections;
using UnityEngine;

namespace RPG.Scenemanagemnt
{    
    public class SavingWrapper : MonoBehaviour
    {
        const string Defaultsavefile = "Dsave";

        private void Awake()
        {
            StartCoroutine(LoadLastScene());
        }


        private IEnumerator LoadLastScene()
        {            
            yield return GetComponent<SavingSystem>().LoadLastScene(Defaultsavefile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediately();
            yield return fader.FadeIn(1f);

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Delete();
            }

        }

        private void Delete()
        {
            GetComponent<SavingSystem>().Delete(Defaultsavefile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(Defaultsavefile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(Defaultsavefile);
        }
    }
}
