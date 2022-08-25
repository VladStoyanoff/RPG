using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control 
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        GameObject player;


        void Start()
        {
            player = GameObject.FindWithTag("Player");
        }
        void Update()
        {
            var playerIsInRange = Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
            if (!playerIsInRange) return;
            Debug.Log(gameObject.name + "is chasing");
        }
    }
}
