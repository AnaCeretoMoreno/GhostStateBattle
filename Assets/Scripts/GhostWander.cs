using UnityEngine;
using System.Collections;

public class GhostWander : MonoBehaviour
{
    public float speed = 2f;
    public float waitTime = 2f;
    public float moveRadius = 5f; // Radio alrededor de la posición inicial
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        while (true)
        {
            targetPosition = GetRandomPosition();
            isMoving = true;

            // Mover hasta llegar al punto
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            isMoving = false;
            yield return new WaitForSeconds(waitTime);
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection.y = 0; // Mantener el movimiento en el plano horizontal
        return transform.position + randomDirection;
    }
}