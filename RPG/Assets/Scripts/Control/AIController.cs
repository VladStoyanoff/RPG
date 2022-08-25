using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Control 
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        GameObject player;
        Fighter fighterScript;


        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighterScript = GetComponent<Fighter>();

        }
        void Update()
        {
            UpdateCombat();

        }

        void UpdateCombat()
        {
            var playerIsInRange = Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
            if (playerIsInRange) fighterScript.Attack(player);
            if (!playerIsInRange) fighterScript.Cancel();
        }
    }
}
