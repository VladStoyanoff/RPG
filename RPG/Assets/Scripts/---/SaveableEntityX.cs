using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.lol 
{
    [ExecuteAlways]
    public class SaveableEntityX : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";
        public string GetUniqueIdentifier()
        {
            return "";
        }

        public object CaptureState()
        {
            Debug.Log("Capturing State for " + GetUniqueIdentifier());
            return null;
        }

        public void RestoreState(object state)
        {
            Debug.Log("Restoring State for " + GetUniqueIdentifier());
        }

        public void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            Debug.Log("Editing");
        }
    }
}
