using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float fadeDuration = 1f;
    public int numPoints = 1;
    public GameObject deathVFXPrefab;

    private Renderer ghostRenderer;
    private bool isFading = false;
    private Coroutine fadingCoroutine;
    private GameObject killer;
    private bool isBeingKilled = false;

    private GhostSpawner ghostSpawner;
    private GhostMovement ghostMovement;
    private GhostAnimationController animationController;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        ghostRenderer = GetComponentInChildren<Renderer>();
        ghostMovement = GetComponent<GhostMovement>();
        animationController = GetComponent<GhostAnimationController>();

        audioManager.PlaySFX(audioManager.normal_spawn);
        animationController.PlayIdle();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerLight") && !isFading && !isBeingKilled)
        {
            killer = other.transform.root.gameObject;
            isBeingKilled = true;

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
            isBeingKilled = false;
        }
    }

    private IEnumerator FadeAndDie()
    {
        isFading = true;
        ghostMovement.StopMovement();

        animationController.PlaySurprised();
        yield return new WaitForSeconds(0.5f);

        animationController.PlayDissolve();

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

        audioManager.PlaySFX(audioManager.normal_death);

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

        ghostMovement.enabled = true;
        ghostMovement.ResumeMovement(); 

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GhostWall"))
        {
            ghostMovement.ReboundFromWall();
        }
    }

    public void SetSpawner(GhostSpawner spawner)
    {
        ghostSpawner = spawner;
    }
}
