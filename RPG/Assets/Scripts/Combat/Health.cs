using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;
        bool isDead;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health > 0) return;
            GetComponentInChildren<UpdateAnimator>().Die();
            HandleDeath();

        }

        void HandleDeath()
        {
            isDead = true;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }

        public bool GetIsDeadBool() => isDead; 
    }
}

