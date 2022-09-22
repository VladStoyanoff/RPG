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
        [SerializeField] bool isRightHanded = true;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (weaponPrefab == null) return;
            if (isRightHanded)
            {
                Instantiate(weaponPrefab, rightHand);
            } 
            else
            {
                Instantiate(weaponPrefab, leftHand);
            }
            if (animator == null) return;
            animator.runtimeAnimatorController = weaponOverrideController;
        }

        public float GetAttackingRange() => attackingRange;
        public float GetWeaponDamage() => weaponDamage;
        public float GetTimeBetweenAttacks() => timeBetweenAttacks;
    }
}
