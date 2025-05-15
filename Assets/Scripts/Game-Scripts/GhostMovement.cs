using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public float changeDirectionInterval = 3f;
    public float houseBoundaryX = 10f;
    public float houseBoundaryZ = 10f;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SetRandomDestination();
        InvokeRepeating(nameof(SetRandomDestination), changeDirectionInterval, changeDirectionInterval);
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            animator.SetBool("IsRunning", false);
        }
        else
        {
            animator.SetBool("IsRunning", true);
        }
    }

    void SetRandomDestination()
    {
        float randomX = Random.Range(-houseBoundaryX, houseBoundaryX);
        float randomZ = Random.Range(-houseBoundaryZ, houseBoundaryZ);
        Vector3 randomPos = new Vector3(randomX, transform.position.y, randomZ);

        agent.SetDestination(randomPos);
    }
}