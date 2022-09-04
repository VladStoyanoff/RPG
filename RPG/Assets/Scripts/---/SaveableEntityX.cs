using RPG.Core;
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
        NavMeshAgent navMesh;

        void Awake()
        {
            navMesh = GetComponent<NavMeshAgent>();
        }

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            return new SerializableVector3X(transform.position);
        }

        public void RestoreState(object state)
        {
            var position = (SerializableVector3X)state;
            navMesh.enabled = false;
            transform.position = position.ToVector();
            navMesh.enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
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
