using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Move;
using System;
using UnityEngine.AI;

namespace RPG.Control 
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float guardingPositionWidth = 1f;
        [SerializeField] float patrollingSpeedFraction = .4f;
        GameObject player;
        Fighter fighterScript;
        Movement movementScript;

        float timeSinceLastSeenPlayer = Mathf.Infinity;
        int currentGuardingPositionIndex = 0;
        float timeOnGuardingPositionPassed = 0f;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighterScript = GetComponent<Fighter>();
            movementScript = GetComponent<Movement>();
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

            movementScript.ActivateNavMeshAgent();
            movementScript.Move(GetCurrentGuardingPosition(), patrollingSpeedFraction);
            if (!AtGuardingPosition()) return;
            var timeOnGuardingPosition = 3;
            timeOnGuardingPositionPassed += Time.deltaTime;
            if (timeOnGuardingPositionPassed < timeOnGuardingPosition) return;
            timeOnGuardingPositionPassed = 0;
            currentGuardingPositionIndex = patrolPath.GetNextGuardingPositionIndex(currentGuardingPositionIndex);
        }

        bool AtGuardingPosition()
        {
            var distanceToGuardingPosition = Vector3.Distance(transform.position, GetCurrentGuardingPosition());
            return distanceToGuardingPosition < guardingPositionWidth;
        }

        Vector3 GetCurrentGuardingPosition()
        {
            return patrolPath.GetGuardingPosition(currentGuardingPositionIndex);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
