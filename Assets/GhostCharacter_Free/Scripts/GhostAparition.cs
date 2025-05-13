using UnityEngine;

public class GhostAparition : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float spawnInterval = 5f;

    public Vector2 minBounds = new Vector2(-35, -35); // límites seguros dentro de la casa
    public Vector2 maxBounds = new Vector2(35, 35);

    void Start()
    {
        InvokeRepeating(nameof(SpawnGhost), 0f, spawnInterval);
    }

    void SpawnGhost()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(minBounds.x, maxBounds.x),
            0.5f,
            Random.Range(minBounds.y, maxBounds.y)
        );

        Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
    }
}