using UnityEngine;

public class GhostAparition : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float spawnRadius = 10f;
    public float spawnInterval = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnGhost), 0f, spawnInterval);
    }

    void SpawnGhost()
    {
        Vector3 spawnPosition = GetRandomPosition();
        Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
        randomOffset.y = 0; // Para mantenerlo en el mismo plano
        return transform.position + randomOffset;
    }
}