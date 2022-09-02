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

        void Awake()
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

        [System.Serializable]
        struct MoverSaveData 
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);

            return data;
        }

        public void RestoreState(object state)
        {
            MoverSaveData data = (MoverSaveData)state;
            DisableNavMeshAgent();
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            ActivateNavMeshAgent();
        }
        #endregion
    }
}