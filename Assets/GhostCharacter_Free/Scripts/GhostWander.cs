using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab;
    public float spawnInterval = 2f;

    public Vector2 minBounds = new Vector2(-40, -40); // l�mites de la casa
    public Vector2 maxBounds = new Vector2(40, 40);

    void Start()
    {
        InvokeRepeating("SpawnGem", 2f, spawnInterval);
    }

    void SpawnGem()
    {
        Vector3 randomPos = new Vector3(
            Random.Range(minBounds.x, maxBounds.x),
            0.5f, // altura de aparici�n
            Random.Range(minBounds.y, maxBounds.y)
        );

        Instantiate(gemPrefab, randomPos, Quaternion.identity);
    }
}