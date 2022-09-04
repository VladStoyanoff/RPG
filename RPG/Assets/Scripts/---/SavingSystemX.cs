using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RPG.lol 
{
    public class SavingSystemX : MonoBehaviour
    {

        public void Save(string saveFile)
        {
            var path = GetPathFromSaveFile(saveFile);
            Debug.Log("Saving to " + path);
            using (var stream = File.Open(path, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, CaptureState());
            }
        }

        public void Load(string saveFile)
        { 
            var path = GetPathFromSaveFile(saveFile);
            Debug.Log("Loading from " + path);
            using (var stream = File.Open(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                RestoreState(formatter.Deserialize(stream));
            }
        }

        object CaptureState()
        {
            Dictionary<string, object> stateDict = new Dictionary<string, object>();
            foreach(var saveableEntity in FindObjectsOfType<SaveableEntityX>())
            {
                stateDict[saveableEntity.GetUniqueIdentifier()] = saveableEntity.CaptureState();
            }
            return stateDict;
        }

        void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (var saveableEntity in FindObjectsOfType<SaveableEntityX>())
            {
                saveableEntity.RestoreState(stateDict[saveableEntity.GetUniqueIdentifier()]);
            }
        }

        string GetPathFromSaveFile(string saveFile) => Path.Combine(Application.persistentDataPath, saveFile + ".sav");
    }
}
