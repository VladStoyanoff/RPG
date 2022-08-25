using UnityEngine;
using UnityEngine.AI;

namespace RPG.Animation
{
    public class UpdateAnimator : MonoBehaviour
    {
        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
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
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        public void Die()
        {
            animator.SetTrigger("die");
        }

        public void StopAttackIfInProcess()
        {
            animator.SetTrigger("stopAttack");
        }
    }
}
