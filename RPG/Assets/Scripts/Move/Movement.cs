using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;

namespace RPG.Move 
{
    public class Movement : MonoBehaviour, IAction, ISaveable
    {

        NavMeshAgent navMeshAgent;
        [SerializeField] float maxSpeed = 6f;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        
        public void MoveToTarget(GameObject target, float speedFraction) 
        {
            if (!navMeshAgent.enabled) return;
            navMeshAgent.destination = target.transform.position;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            ActivateNavMeshAgent();
        }

        public void Move(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            if (!navMeshAgent.enabled) return;
            navMeshAgent.destination = destination;
            ActivateNavMeshAgent();
        }

        public void DisableNavMeshAgent()
        {
            if (!navMeshAgent.enabled) return;
            navMeshAgent.isStopped = true;
        }

        public void ActivateNavMeshAgent()
        {
            navMeshAgent.isStopped = false;
        }

        #region Saving
        public object CaptureState() => new SerializableVector3(transform.position);

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            Start();
            DisableNavMeshAgent();
            transform.position = position.ToVector();
            ActivateNavMeshAgent();
        }
        #endregion
    }
}