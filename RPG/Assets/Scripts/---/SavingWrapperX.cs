using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.lol;

public class SavingWrapperX : MonoBehaviour
{
    SavingSystemX savingSystemScriptX;
    const string defaultSaveFile = "save";

    void Awake()
    {
        savingSystemScriptX = GetComponent<SavingSystemX>();
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
            savingSystemScriptX.Save(defaultSaveFile);
        }
    }

    public void Load()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            savingSystemScriptX.Load(defaultSaveFile);
        }
    }
}
