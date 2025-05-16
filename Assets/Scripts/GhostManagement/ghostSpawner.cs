using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostSpawner : MonoBehaviour
{
    public bool canSpawn = true;
    public GameObject ghostPrefab;
    public GameObject specialGhostPrefab;
    public List<Transform> ghostSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns = 5f;
    public float specialGhostSpawnInterval = 20f;

    private List<GameObject> ghostList = new List<GameObject>();
    private List<GameObject> specialGhostList = new List<GameObject>();

    private float specialGhostTimer = 0f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        if (!canSpawn) return;

        specialGhostTimer += Time.deltaTime;

        if (specialGhostTimer >= specialGhostSpawnInterval)
        {
            SpawnSpecialGhost();
            specialGhostTimer = 0f;
        }
    }

    private void SpawnGhost()
    {
        if (ghostSpawnPositions.Count == 0 || ghostPrefab == null) return;

        Vector3 randomPosition = ghostSpawnPositions[Random.Range(0, ghostSpawnPositions.Count)].position;
        GameObject ghost = Instantiate(ghostPrefab, randomPosition, ghostPrefab.transform.rotation);
        ghostList.Add(ghost);

        Ghost ghostScript = ghost.GetComponent<Ghost>();
        if (ghostScript != null)
            ghostScript.SetSpawner(this);
    }

    private void SpawnSpecialGhost()
    {
        if (ghostSpawnPositions.Count == 0 || specialGhostPrefab == null) return;

        Vector3 randomPosition = ghostSpawnPositions[Random.Range(0, ghostSpawnPositions.Count)].position;
        GameObject specialGhost = Instantiate(specialGhostPrefab, randomPosition, specialGhostPrefab.transform.rotation);
        specialGhostList.Add(specialGhost);

        SpecialGhost specialGhostScript = specialGhost.GetComponent<SpecialGhost>();
        if (specialGhostScript != null)
            specialGhostScript.SetSpawner(this);
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnGhost();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void RemoveGhostFromList(GameObject ghost)
    {
        if (ghostList.Contains(ghost))
            ghostList.Remove(ghost);
    }

    public void RemoveSpecialGhostFromList(GameObject specialGhost)
    {
        if (specialGhostList.Contains(specialGhost))
            specialGhostList.Remove(specialGhost);
    }

    public void DestroyAllGhosts()
    {
        foreach (GameObject ghost in ghostList)
        {
            Destroy(ghost);
        }
        ghostList.Clear();

        foreach (GameObject specialGhost in specialGhostList)
        {
            Destroy(specialGhost);
        }
        specialGhostList.Clear();
    }
}
