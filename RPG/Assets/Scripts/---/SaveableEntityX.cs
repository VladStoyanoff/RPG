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

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            var state = new Dictionary<string, object>();
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        public void RestoreState(object state)
        {
            var stateDict = (Dictionary<string, object>)state;
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                var typeString = saveable.GetType().ToString();
                if (stateDict.ContainsKey(typeString) == false) continue;
                saveable.RestoreState(stateDict[typeString]);
            }
        }

#if UNITY_EDITOR
        public void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            var serializedObject = new SerializedObject(this);
            var property = serializedObject.FindProperty("uniqueIdentifier");

            if (property.stringValue == "")
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}
