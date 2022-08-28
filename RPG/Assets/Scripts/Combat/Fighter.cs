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

        bool isInRangeOfTarget;

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
            if (gameObject.tag == "Player")
            {
                movementScript.MoveToTarget(healthScriptOfTarget.gameObject, 1f);
            }
            else if (gameObject.tag == "Enemy")
            {
                movementScript.MoveToTarget(healthScriptOfTarget.gameObject, 0.8f);
            }
            isInRangeOfTarget = Vector3.Distance(transform.position, healthScriptOfTarget.transform.position) < attackingRange;
            if (!isInRangeOfTarget) return;
            movementScript.Cancel();
        }

        void AttackBehaviour()
        {
            if (!isInRangeOfTarget) return;
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
            GetComponent<Movement>().Cancel();
        }

        public Health GetSelectedTarget() => healthScriptOfTarget;
        public float GetWeaponDamage() => weaponDamage;
    }
}