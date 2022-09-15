using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.lol 
{
    [ExecuteAlways]
    public class SaveableEntityX : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";
        static Dictionary<string, SaveableEntityX> globalLookup = new Dictionary<string, SaveableEntityX>();


        public object CaptureState()
        {
            var state = new Dictionary<string, object>();
            foreach (var saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        public void RestoreState(object state)
        {
            var stateDict = (Dictionary<string, object>)state;
            foreach (var saveable in GetComponents<ISaveable>())
            {
                var typeString = saveable.GetType().ToString();
                if (stateDict.ContainsKey(typeString) == false) continue;
                saveable.RestoreState(stateDict[typeString]);
            }
        }

        public void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            var serializedObject = new SerializedObject(this);
            var property = serializedObject.FindProperty("uniqueIdentifier");

            if (string.IsNullOrEmpty(property.stringValue) || IsUnique(property.stringValue) == false)
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLookup[property.stringValue] = this;
        }

        bool IsUnique(string candidate)
        {
            if (globalLookup.ContainsKey(candidate) == false) return true;
            if (globalLookup[candidate] == this) return true;
            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }
            return false;

        }

        public string GetUniqueIdentifier() => uniqueIdentifier;
    }
}
