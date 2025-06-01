using UnityEngine;

public class AutodestroyParticles : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration + 0.5f);
    }
}
