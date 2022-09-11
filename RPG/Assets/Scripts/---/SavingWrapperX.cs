using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.lol 
{
    public class SavingWrapperX : MonoBehaviour
    {
        SavingSystemX savingSystemScriptX;
        const string defaultSaveFile = "save";

        void Awake()
        {
            savingSystemScriptX = GetComponent<SavingSystemX>();
        }

        void Start()
        {
            Load();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Save()
        {
            savingSystemScriptX.Save(defaultSaveFile);
        }

        public void Load()
        {
            savingSystemScriptX.Load(defaultSaveFile);
        }
    }
}


