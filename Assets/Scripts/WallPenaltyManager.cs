using System.Collections;
using UnityEngine;

public class WallPenaltyManager : MonoBehaviour
{
    [SerializeField] private GameObject lightTrigger;
    [SerializeField] private Light playerLight;
    [SerializeField] private float penaltyDuration = 5f;
    [SerializeField] private float blinkFrequency = 5f;

    private bool isPenalized = false;

    public void StartPenalty()
    {
        if (!isPenalized)
        {
            StartCoroutine(Penalize());
        }
    }

    private IEnumerator Penalize()
    {
        isPenalized = true;

        if (lightTrigger != null)
            lightTrigger.SetActive(false);

        float elapsed = 0f;
        while (elapsed < penaltyDuration)
        {
            if (playerLight != null)
                playerLight.enabled = !playerLight.enabled;

            yield return new WaitForSeconds(1f / blinkFrequency);
            elapsed += 1f / blinkFrequency;
        }

        if (playerLight != null)
            playerLight.enabled = true;

        if (lightTrigger != null)
            lightTrigger.SetActive(true);

        isPenalized = false;
    }
}
