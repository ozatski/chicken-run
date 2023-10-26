using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenController : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float fleeDistance = 5.0f; // Distance at which the chicken starts to flee

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = transform.position - player.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < fleeDistance)
        {
            // Calculate a destination point that is opposite to the player's position
            Vector3 fleeDestination = transform.position + directionToPlayer.normalized * fleeDistance;

            // Set the agent's destination to the flee point
            agent.SetDestination(fleeDestination);
        }
    }
}
