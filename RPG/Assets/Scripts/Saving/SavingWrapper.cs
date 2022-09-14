using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement 
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        [SerializeField] float fadeInTime = 1f;
        SavingSystem savingSystemScript;

        void Awake()
        {
            savingSystemScript = GetComponent<SavingSystem>();
        }

        IEnumerator Start()
        {
            var fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);
        }

        void Update()
        {
            Save();
            Load();
        }

        public void Save()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                savingSystemScript.Save(defaultSaveFile);
            }
        }

        public void Load()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                savingSystemScript.Load(defaultSaveFile);
            }
        }
    }
}
