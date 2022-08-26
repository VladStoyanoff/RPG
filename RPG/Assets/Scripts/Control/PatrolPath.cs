using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control 
{
    public class PatrolPath : MonoBehaviour
    {
        void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(transform.GetChild(i).position, .5f);
            }
        }
    }
}
