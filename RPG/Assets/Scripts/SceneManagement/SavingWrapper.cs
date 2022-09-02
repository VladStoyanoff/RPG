using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement 
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        [SerializeField] float fadeInTime = 5f;
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
            if (Input.GetKeyDown(KeyCode.L))
            {
                savingSystemScript.Load(defaultSaveFile);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                savingSystemScript.Save(defaultSaveFile);
            }
        }
    }
}
