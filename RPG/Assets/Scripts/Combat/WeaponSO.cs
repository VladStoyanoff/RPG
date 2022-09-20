using UnityEngine;

namespace RPG.Combat 
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController weaponOverrideController;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] float attackingRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 20f;

        public void Spawn(Transform handTransform, Animator animator)
        {
            Instantiate(weaponPrefab, handTransform);
            animator.runtimeAnimatorController = weaponOverrideController;
        }

        public float GetAttackingRange() => attackingRange;
        public float GetWeaponDamage() => weaponDamage;
        public float GetTimeBetweenAttacks() => timeBetweenAttacks;
    }
}
