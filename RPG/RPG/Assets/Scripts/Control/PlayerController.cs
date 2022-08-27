using System;
using UnityEngine;
using RPG.Move;
using RPG.Combat;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour 
    {

        [Header("Masks")]
        [SerializeField] LayerMask layerMask = new LayerMask();

        void Update() 
        {
            if (GetComponent<Health>().GetIsDeadBool()) return;
            if (UpdateCombat()) return;
            if (UpdateControlMovement()) return;
            Debug.Log("Nothing to do");
        }

        bool UpdateControlMovement()
        {
            if (!Input.GetMouseButton(1)) return false;
            if (!Physics.Raycast(GetMouseRay(), out RaycastHit hit, float.MaxValue, layerMask)) return false;
            GetComponent<Movement>().Move(hit.point);
            return true;
        }

        bool UpdateCombat() 
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits) 
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!Input.GetMouseButton(1)) return false;
                GetComponent<Fighter>().StartAttackAction(target.gameObject);
                return true;
            }
            return false;
        }

        static Ray GetMouseRay() => Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}

