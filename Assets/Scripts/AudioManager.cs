using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------Audio Sources------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------Audio Clips------")]
    public AudioClip backgroundMusic;
    public AudioClip normal_death;
    public AudioClip special_death;
    public AudioClip nomal_spawn;
    public AudioClip special_spawn;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
