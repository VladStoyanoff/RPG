using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics 
{
    public class IntroSequence : MonoBehaviour
    {
        bool alreadyTriggered;

        void OnTriggerEnter(Collider other)
        {
            if (!alreadyTriggered && other.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                alreadyTriggered = true;
            }
        }
    }
}
