using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Animation;
using RPG.Saving;

namespace RPG.Combat
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100f;
        bool isDead;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health > 0) return;
            HandleDeath();
        }

        void HandleDeath()
        {
            isDead = true;
            GetComponentInChildren<UpdateAnimator>().Die();
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }

        public bool GetIsDeadBool() => isDead;

        #region Saving
        public object CaptureState() => health;

        public void RestoreState(object state)
        {
            health = (float)state;
            if (health > 0) return;
            HandleDeath();
        }
        #endregion
    }
}

