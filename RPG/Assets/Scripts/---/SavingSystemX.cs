using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RPG.lol 
{
    public class SavingSystemX : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            Debug.Log("Saving to " + GetPathFromSaveFile(saveFile));
        }

        public void Load(string saveFile)
        {
            Debug.Log("Loading from " + GetPathFromSaveFile(saveFile));
        }

        string GetPathFromSaveFile(string saveFile) => Path.Combine(Application.persistentDataPath, saveFile + ".sav");
    }
}
