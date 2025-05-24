using UnityEngine;

public class autodestroyParticles : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration + 0.5f);
    }
}
