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
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
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

        void CaptureState(Dictionary<string, object> state)
        {
            foreach (var saveableEntity in FindObjectsOfType<SaveableEntityX>())
            {
                state[saveableEntity.GetUniqueIdentifier()] = saveableEntity.CaptureState();
            }
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
                var uniqueId = saveableEntity.GetUniqueIdentifier();
                if (state.ContainsKey(uniqueId) == false) continue;
                saveableEntity.RestoreState(state[uniqueId]);
            }
        }

        Dictionary<string, object> LoadFile(string saveFile)
        {

            var path = GetPathFromSaveFile(saveFile);
            if (File.Exists(path) == false) return new Dictionary<string, object>();
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
