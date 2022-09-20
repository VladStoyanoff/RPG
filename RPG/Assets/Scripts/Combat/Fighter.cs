using UnityEngine;
using RPG.Move;
using RPG.Core;
using RPG.Animation;

namespace RPG.Combat 
{
    public class Fighter : MonoBehaviour, IAction
    {
        [Header("Attack")]
        [SerializeField] WeaponSO weapon;
        [SerializeField] Transform handTransform = null;

        float timeSinceLastAttack = Mathf.Infinity;

        [Header("Scripts")]
        Movement movementScript;
        Health healthScriptOfAttacker;
        UpdateAnimator updateAnimatorScript;
        Health healthScriptOfTarget;

        bool isInRangeOfTarget;

        void Awake()
        {
            movementScript = GetComponent<Movement>();
            updateAnimatorScript = GetComponentInChildren<UpdateAnimator>();
            healthScriptOfAttacker = GetComponent<Health>();
        }

        void Start()
        {
            SpawnWeapon();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            UpdateMoveToAttack();
        }

        void SpawnWeapon()
        {
            if (weapon == null) return;
            var animator = GetComponentInChildren<Animator>();
            weapon.Spawn(handTransform, animator);
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
            movementScript.StopAttack();
        }

        void AttackBehaviour()
        {
            if (!isInRangeOfTarget) return;
            transform.LookAt(healthScriptOfTarget.transform);
            if (timeSinceLastAttack < weapon.GetTimeBetweenAttacks()) return;
            healthScriptOfTarget.TakeDamage(weapon.GetWeaponDamage());
            updateAnimatorScript.AttackAnimation();
            timeSinceLastAttack = 0;
        }

        public void StopAttack()
        {
            updateAnimatorScript.StopAttackIfInProcess();
            healthScriptOfTarget = null;
            GetComponent<Movement>().StopAttack();
        }

        public Health GetSelectedTarget() => healthScriptOfTarget;
    }
}