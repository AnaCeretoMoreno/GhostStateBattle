using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float wanderSpeed = 2.5f;        // Más rápido
    public float wanderRadius = 4f;         // Más amplio
    public float fadeDuration = 1f;

    private Vector3 targetPosition;
    private Renderer ghostRenderer;
    private bool isFading = false;

    private ghostSpawner ghostSpawner;
    private Coroutine fadingCoroutine;

    private GameObject killer;

    public int numPoints = 1;

    public GameObject deathVFXPrefab;
    public AudioClip spawnSound;
    public AudioClip deathSound;

    private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
    private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
    private static readonly int SurprisedState = Animator.StringToHash("Base Layer.surprised");
    private static readonly int AttackState = Animator.StringToHash("Base Layer.attack_shift");
    private static readonly int DissolveState = Animator.StringToHash("Base Layer.dissolve");
    private static readonly int AttackTag = Animator.StringToHash("Attack");

    private Animator anim;


    private Vector3 lastDirection; // Para reflejar dirección al chocar con GhostWall



    void Start()
    {
        ghostRenderer = GetComponentInChildren<Renderer>();
        SetNewTargetPosition();
        StartCoroutine(Wander());
        anim = GetComponent<Animator>();


        if (spawnSound != null)
        {
            AudioSource.PlayClipAtPoint(spawnSound, transform.position);
        }

        anim.CrossFade(IdleState, 0.1f, 0, 0);
    }

    void Update()
    {
        if (!isFading)
        {
            MoveGhost();
        }
    }

    void MoveGhost()
    {
        Vector3 direction = targetPosition - transform.position;
        lastDirection = direction.normalized;

        if (direction.magnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, wanderSpeed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            anim.CrossFade(MoveState, 0.1f);

        }
        else
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection.y = 0;
        targetPosition = transform.position + randomDirection;
    }

    private IEnumerator Wander()
    {
        while (!isFading)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            SetNewTargetPosition();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerLight") && !isFading)
        {
            killer = other.transform.root.gameObject;
            if (fadingCoroutine == null)
            {
                fadingCoroutine = StartCoroutine(FadeAndDie());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerLight") && fadingCoroutine != null)
        {
            StopCoroutine(fadingCoroutine);
            fadingCoroutine = null;
            ResetTransparency();
        }
    }

    private IEnumerator FadeAndDie()
    {
        isFading = true;
        anim.CrossFade(SurprisedState, 0.1f);
        yield return new WaitForSeconds(0.5f); // tiempo para mostrar la reacción

        anim.CrossFade(DissolveState, 0.1f);

        float elapsed = 0f;
        Material mat = ghostRenderer.material;
        Color originalColor = mat.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            mat.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        if (deathVFXPrefab != null)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        }

        if (killer != null)
        {
            GameStateManager.Instance.AddScore(killer, numPoints);
        }

        if (ghostSpawner != null)
        {
            ghostSpawner.RemoveGhostFromList(gameObject);
        }

        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }


        Destroy(gameObject);
    }

    private void ResetTransparency()
    {
        isFading = false;
        if (ghostRenderer != null)
        {
            Color col = ghostRenderer.material.color;
            ghostRenderer.material.color = new Color(col.r, col.g, col.b, 1f);
        }
    }

    public void SetSpawner(ghostSpawner spawner)
    {
        ghostSpawner = spawner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GhostWall"))
        {
            // Rebote simple: mover en dirección opuesta
            Vector3 rebound = -lastDirection.normalized * wanderRadius;
            targetPosition = transform.position + rebound;
        }
    }
}
