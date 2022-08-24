using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;

namespace RPG.Core
{
    public class UpdateAnimator : MonoBehaviour
    {
        Animator animator;
        Fighter fighterScript;

        void Start()
        {
            animator = GetComponent<Animator>();
            fighterScript = GetComponentInParent<Fighter>();
        }

        void Update()
        {
            UpdateAnimation();
        }

        void UpdateAnimation()
        {
            var velocity = GetComponentInParent<NavMeshAgent>().velocity;
            var localVelocity = transform.InverseTransformDirection(velocity);
            var speed = localVelocity.z;
            animator.SetFloat("ForwardSpeed", speed);
        }

        public void AttackAnimation()
        {
            animator.SetTrigger("attack");
        }

        public void Die()
        {
            animator.SetTrigger("die");
        }

        // Animation Event
        void Hit()
        {
            fighterScript.GetSelectedTarget().GetComponent<Health>().TakeDamage(fighterScript.GetWeaponDamage());
        }
    }
}
