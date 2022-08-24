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
        UpdateAnimator updateAnimatorScript;
        Health target;

        void Start()
        {
            movementScript = GetComponent<Movement>();
            updateAnimatorScript = GetComponentInChildren<UpdateAnimator>();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            UpdateMoveToAttack();
        }

        public void Attack(CombatTarget combatTarget) 
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        void UpdateMoveToAttack()
        {
            if (target == null) return;
            movementScript.MoveToTarget(target.gameObject);
            bool isInRange = Vector3.Distance(transform.position, target.transform.position) < attackingRange;
            if (!isInRange) return;
            movementScript.Cancel();

            if (target.GetIsDeadBool()) return;
            AttackBehaviour();
        }

        void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack < timeBetweenAttacks) return;
            // This will trigger the Hit() animation event inside UpdateAnimator.cs
            updateAnimatorScript.AttackAnimation();
            timeSinceLastAttack = 0;
        }

        public void Cancel()
        {
            updateAnimatorScript.StopAttackIfInProcess();
            target = null;
        }


        public Health GetSelectedTarget() => target;
        public float GetWeaponDamage() => weaponDamage;
    }
}