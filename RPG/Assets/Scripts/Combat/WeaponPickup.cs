using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat 
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] WeaponSO sword;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") == false) return;
            other.GetComponent<Fighter>().EquipWeapon(sword);
            Destroy(gameObject);
        }
    }
}
