using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistantObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistantObejctPrefab;
        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) { return; }
            SpawnPersistantObject();
            hasSpawned = true;
        }

        private void SpawnPersistantObject()
        {
            GameObject persistantObject = Instantiate(persistantObejctPrefab);
            DontDestroyOnLoad(persistantObject);
        }
    }

}