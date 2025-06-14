using System.Collections;
using UnityEngine;

public class DirectionalLightController : MonoBehaviour
{
    private Light directionalLight;
    private Coroutine transitionCoroutine;

    //private float lightOnIntensity = 4449.912f;
    private float lightOnIntensity_progressive = 500f;
    private float lightOffIntensity = 0.05f;
    private float transitionDuration = 1.5f;

    void Awake()
    {
        directionalLight = GetComponent<Light>();

        if (directionalLight == null)
        {
            Debug.LogError("No Light component found on DirectionalLightController!");
        }
    }

    public void TurnOnLight()
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(ChangeLightIntensity(lightOnIntensity_progressive));
    }

    public void TurnOffLight()
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(ChangeLightIntensity(lightOffIntensity));
    }

    private IEnumerator ChangeLightIntensity(float targetIntensity)
    {
        float startIntensity = directionalLight.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            directionalLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            yield return null;
        }

        directionalLight.intensity = targetIntensity;
        transitionCoroutine = null;
    }
}
