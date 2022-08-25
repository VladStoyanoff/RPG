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
        float timeSinceLastAttack = 0f;

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
            movementScript.MoveToTarget(healthScriptOfTarget.gameObject);
            var isInRange = Vector3.Distance(transform.position, healthScriptOfTarget.transform.position) < attackingRange;
            if (!isInRange) return;
            movementScript.Cancel();

            AttackBehaviour();
        }

        void AttackBehaviour()
        {
            if (healthScriptOfTarget.GetIsDeadBool()) return;
            transform.LookAt(healthScriptOfTarget.transform);
            if (timeSinceLastAttack < timeBetweenAttacks) return;
            updateAnimatorScript.AttackAnimation();
            Invoke("InflictDamage", .5f);
            timeSinceLastAttack = 0;
        }

        void InflictDamage()
        {
            healthScriptOfTarget.TakeDamage(GetWeaponDamage());
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