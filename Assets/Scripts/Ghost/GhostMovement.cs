using System.Collections;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float wanderSpeed = 2.5f;
    public float wanderRadius = 4f;

    private Vector3 targetPosition;
    private Vector3 lastDirection;
    private Coroutine wanderCoroutine;
    private bool isStopped = false;

    private GhostAnimationController animationController;

    private void Start()
    {
        SetNewTargetPosition();
        animationController = GetComponent<GhostAnimationController>();
        wanderCoroutine = StartCoroutine(WanderRoutine());
    }

    private void Update()
    {
        if (isStopped) return;

        MoveGhost();
    }

    private void MoveGhost()
    {
        Vector3 direction = targetPosition - transform.position;
        lastDirection = direction.normalized;

        if (direction.magnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, wanderSpeed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            animationController.PlayMove();
        }
        else
        {
            SetNewTargetPosition();
        }
    }

    private void SetNewTargetPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection.y = 0;
        targetPosition = transform.position + randomDirection;
    }

    private IEnumerator WanderRoutine()
    {
        while (!isStopped)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            SetNewTargetPosition();
        }
    }

    public void StopMovement()
    {
        isStopped = true;
        if (wanderCoroutine != null)
            StopCoroutine(wanderCoroutine);
    }

    public void ReboundFromWall()
    {
        targetPosition = transform.position - lastDirection.normalized * wanderRadius;
    }
}
