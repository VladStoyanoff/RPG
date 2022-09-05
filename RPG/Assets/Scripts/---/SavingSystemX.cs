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
        #region SAVING
        public void Save(string saveFile)
        {
            SaveFile(saveFile, CaptureState());
        }

        void SaveFile(string saveFile, Dictionary<string, object> state)
        {
            var path = GetPathFromSaveFile(saveFile);
            using (var stream = File.Open(path, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        Dictionary<string, object> CaptureState()
        {
            Dictionary<string, object> stateDict = new Dictionary<string, object>();
            foreach (var saveableEntity in FindObjectsOfType<SaveableEntityX>())
            {
                stateDict[saveableEntity.GetUniqueIdentifier()] = saveableEntity.CaptureState();
            }
            return stateDict;
        }
        #endregion

        #region LOADING
        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        void RestoreState(Dictionary<string, object> state)
        {
            foreach (var saveableEntity in FindObjectsOfType<SaveableEntityX>())
            {
                saveableEntity.RestoreState(state[saveableEntity.GetUniqueIdentifier()]);
            }
        }

        Dictionary<string, object> LoadFile(string saveFile)
        {
            var path = GetPathFromSaveFile(saveFile);
            using (var stream = File.Open(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }
        #endregion

        string GetPathFromSaveFile(string saveFile) => Path.Combine(Application.persistentDataPath, saveFile + ".sav");
    }
}
