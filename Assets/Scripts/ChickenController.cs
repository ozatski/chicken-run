using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenController : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float fleeDistance = 5.0f; // Distance at which the chicken starts to flee
    public float wanderRadius = 10.0f; // for random destinations
    public Animator animator;
    private Vector3 randomDestination;
    private bool isMoving = false;
    public float wanderInterval = 5.0f; // Time between wander movements


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
        StartCoroutine(Wander());
    }



    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 directionToPlayer = transform.position - player.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Check if the agent is moving
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            // Agent is moving, set the "Run" parameter to true
            animator.SetBool("Run", true);
        }
        else
        {
            // Agent is not moving, set the "Run" parameter to false (assuming you have an "Idle" animation state)
            animator.SetBool("Run", false);
        }

        if (distanceToPlayer < fleeDistance)
        {
            // Calculate a destination point that is opposite to the player's position
            Vector3 fleeDestination = transform.position + directionToPlayer.normalized * fleeDistance;

            // Set the agent's destination to the flee point
            agent.SetDestination(fleeDestination);
        }

      



        if (isMoving)
        {
            // Check if the chicken has reached its random destination
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                isMoving = false;
            }
        }


    }

    void SetRandomDestination()
    {
        // Generate a random point within the specified radius
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, 1);
        randomDestination = hit.position;
    }

    IEnumerator Wander()
    {
        while (true)
        {
            if (!isMoving)
            {
                SetRandomDestination();
                agent.SetDestination(randomDestination);
                isMoving = true;
            }
            yield return new WaitForSeconds(wanderInterval);
        }
    }

}
