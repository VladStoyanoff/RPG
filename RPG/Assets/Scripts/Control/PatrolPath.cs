using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmosRadius = .5f;

        void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextGuardingPositionIndex(i);
                Gizmos.DrawSphere(GetGuardingPosition(i), waypointGizmosRadius);
                Gizmos.DrawLine(GetGuardingPosition(i), GetGuardingPosition(j));
            }
        }

        public int GetNextGuardingPositionIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetGuardingPosition(int i) => transform.GetChild(i).position;
    }
}
