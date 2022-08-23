using UnityEngine;
using RPG.Move;
using RPG.Core;

namespace RPG.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float attackingRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 20f;
        float timeSinceLastAttack = 0f;

        Movement movementScript;
        Transform target;

        void Start()
        {
            movementScript = GetComponent<Movement>();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            UpdateMoveToAttack();
        }

        public void Attack(CombatTarget combatTarget) 
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        void UpdateMoveToAttack()
        {
            if (target == null) return;
            movementScript.MoveToTarget(target);
            bool isInRange = Vector3.Distance(transform.position, target.position) < attackingRange;
            if (!isInRange) return;
            movementScript.Cancel();
            AttackBehaviour();
        }

        void AttackBehaviour()
        {
            // This will trigger the Hit() animation event inside UpdateAnimator.cs
            if (timeSinceLastAttack < timeBetweenAttacks) return;
            GetComponentInChildren<UpdateAnimator>().AttackAnimation();
            timeSinceLastAttack = 0;
        }

            public void Cancel()
        {
            target = null;
        }


        public Transform GetSelectedTarget() => target;
        public float GetWeaponDamage() => weaponDamage;
    }
}