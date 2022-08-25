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
        UpdateAnimator updateAnimatorScript;
        Health healthScriptOfTarget;

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

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            healthScriptOfTarget = combatTarget.GetComponent<Health>();
        }

        void UpdateMoveToAttack()
        {
            if (healthScriptOfTarget == null) return;
            if (GetComponent<Health>().GetIsDeadBool()) return;
            if (healthScriptOfTarget.GetIsDeadBool()) return;
            movementScript.MoveToTarget(healthScriptOfTarget.gameObject);
            var isInRange = Vector3.Distance(transform.position, healthScriptOfTarget.transform.position) < attackingRange;
            if (!isInRange) return;
            movementScript.Cancel();

            AttackBehaviour();
        }

        void AttackBehaviour()
        {
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