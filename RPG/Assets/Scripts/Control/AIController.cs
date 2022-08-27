using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Move;
using System;

namespace RPG.Control 
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        GameObject player;
        Fighter fighterScript;
        Movement movementScript;

        Vector3 guardPosition;
        float timeSinceLastSeenPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;


        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighterScript = GetComponent<Fighter>();
            movementScript = GetComponent<Movement>();

            guardPosition = transform.position;

        }
        void Update()
        {
            UpdateCombat();
        }

        void UpdateCombat()
        {
            var playerIsInRange = Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
            if (playerIsInRange)
            {
                AttackBehaviour();
            }

            if (!playerIsInRange)
            {
                SuspiciousBehaviour();
                PatrolBehaviour();
            }
        }

        void AttackBehaviour()
        {
            fighterScript.StartAttackAction(player);
            timeSinceLastSeenPlayer = 0;
        }

        void SuspiciousBehaviour()
        {
            timeSinceLastSeenPlayer += Time.deltaTime;
            fighterScript.Cancel();
            movementScript.Cancel();
        }

        void PatrolBehaviour()
        {
            if (timeSinceLastSeenPlayer < suspicionTime) return;
            var nextPosition = guardPosition;
            movementScript.ActivateNavMeshAgent();
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            movementScript.Move(nextPosition);
        }

        bool AtWaypoint()
        {
            var distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
