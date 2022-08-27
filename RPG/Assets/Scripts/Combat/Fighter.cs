using UnityEngine;
using RPG.Move;
using RPG.Core;
using RPG.Animation;

namespace RPG.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [Header("Attack")]
        [SerializeField] float attackingRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 20f;
        float timeSinceLastAttack = Mathf.Infinity;

        [Header("Scripts")]
        Movement movementScript;
        Health healthScriptOfAttacker;
        UpdateAnimator updateAnimatorScript;
        Health healthScriptOfTarget;

        bool isInRangeOfPlayer;

        void Start()
        {
            movementScript = GetComponent<Movement>();
            updateAnimatorScript = GetComponentInChildren<UpdateAnimator>();
            healthScriptOfAttacker = GetComponent<Health>();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            UpdateMoveToAttack();
        }

        public void StartAttackAction(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            healthScriptOfTarget = combatTarget.GetComponent<Health>();
        }

        void UpdateMoveToAttack()
        {
            if (healthScriptOfTarget == null) return;
            if (healthScriptOfTarget == healthScriptOfAttacker) return;
            if (healthScriptOfAttacker.GetIsDeadBool()) return;
            if (healthScriptOfTarget.GetIsDeadBool()) return;

            MoveToTargetBehaviour();
            AttackBehaviour();
        }

        void MoveToTargetBehaviour()
        {
            movementScript.MoveToTarget(healthScriptOfTarget.gameObject);
            isInRangeOfPlayer = Vector3.Distance(transform.position, healthScriptOfTarget.transform.position) < attackingRange;
            if (isInRangeOfPlayer)
            {
                movementScript.Cancel();
            }
        }

        void AttackBehaviour()
        {
            if (!isInRangeOfPlayer) return;
            transform.LookAt(healthScriptOfTarget.transform);
            if (timeSinceLastAttack < timeBetweenAttacks) return;
            healthScriptOfTarget.TakeDamage(GetWeaponDamage());
            updateAnimatorScript.AttackAnimation();
            timeSinceLastAttack = 0;
        }

        public void Cancel()
        {
            updateAnimatorScript.StopAttackIfInProcess();
            healthScriptOfTarget = null;
        }

        public Health GetSelectedTarget() => healthScriptOfTarget;
        public float GetWeaponDamage() => weaponDamage;
    }
}