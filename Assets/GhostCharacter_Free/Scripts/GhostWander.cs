using UnityEngine;

public class GhostWander : MonoBehaviour
{
    public Vector3 areaSize = new Vector3(18, 0, 18); // Tamaño del área donde se moverá
    public float speed = 2f;
    public float waitTime = 2f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        SetNewTarget();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                Invoke(nameof(SetNewTarget), waitTime);
            }
        }
    }

    void SetNewTarget()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            0,
            Random.Range(-areaSize.z / 2, areaSize.z / 2)
        );

        targetPosition = transform.position + randomOffset;
        isMoving = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}