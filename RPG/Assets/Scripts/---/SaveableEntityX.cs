using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.lol 
{
    public class SaveableEntityX : MonoBehaviour
    {
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
    }
}
