using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Move;

namespace RPG.Control 
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        GameObject player;
        Fighter fighterScript;

        Vector3 guardPosition;
        float timeSinceLastSeenPlayer = Mathf.Infinity;


        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighterScript = GetComponent<Fighter>();

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
            if (timeSinceLastSeenPlayer < suspicionTime) return;
            GetComponent<Movement>().Move(guardPosition);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
