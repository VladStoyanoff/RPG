using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Move 
{
    public class Movement : MonoBehaviour, IAction
    {

        NavMeshAgent navMeshAgent;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        
        public void MoveToTarget(GameObject target) 
        {
            if (!navMeshAgent.enabled) return;
            navMeshAgent.destination = target.transform.position;
            ActivateNavMeshAgent();
        }

        public void Move(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            if (!navMeshAgent.enabled) return;
            navMeshAgent.destination = destination;
            ActivateNavMeshAgent();
        }

        public void Cancel()
        {
            if (!navMeshAgent.enabled) return;
            navMeshAgent.isStopped = true;
        }

        public void ActivateNavMeshAgent()
        {
            navMeshAgent.isStopped = false;
        }
    }
}