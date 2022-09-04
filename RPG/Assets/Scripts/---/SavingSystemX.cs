using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

            }
        }

        public void Load(string saveFile)
        {
            var path = GetPathFromSaveFile(saveFile);
            Debug.Log("Loading from " + path);
            using (var stream = File.Open(path, FileMode.Open))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                Debug.Log(Encoding.UTF8.GetString(buffer));
            }
        }

        string GetPathFromSaveFile(string saveFile) => Path.Combine(Application.persistentDataPath, saveFile + ".sav");
    }
}
