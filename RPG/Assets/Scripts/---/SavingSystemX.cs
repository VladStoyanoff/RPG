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
                var position = new SerializableVector3X(GetPlayerTransform().position);
                formatter.Serialize(stream, position);
            }
        }

        public void Load(string saveFile)
        { 
            var path = GetPathFromSaveFile(saveFile);
            Debug.Log("Loading from " + path);
            using (var stream = File.Open(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                var position = (SerializableVector3X)formatter.Deserialize(stream);
                GetPlayerTransform().position = position.ToVector();
            }
        }

        byte[] SerializeVector(Vector3 vector)
        {
            // a float takes up 4 bytes
            var vectorBytes = new byte[3 * 4];
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);
            return vectorBytes;
        }

        Vector3 DeserializeVector(byte[] buffer)
        {
            var deserializedVector = new Vector3();
            deserializedVector.x = BitConverter.ToSingle(buffer, 0);
            deserializedVector.y = BitConverter.ToSingle(buffer, 4);
            deserializedVector.z = BitConverter.ToSingle(buffer, 8);
            return deserializedVector;
        }

        Transform GetPlayerTransform() => GameObject.FindWithTag("Player").transform;
        string GetPathFromSaveFile(string saveFile) => Path.Combine(Application.persistentDataPath, saveFile + ".sav");
    }
}
