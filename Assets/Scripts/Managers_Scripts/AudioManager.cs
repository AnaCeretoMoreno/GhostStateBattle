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
    public AudioClip normal_spawn;
    public AudioClip special_spawn;

    [Header("-------Audio Player------")]
    public AudioClip penalize_sound;
    public AudioClip player_wins;

    [Header("-------Audio Rug------")]
    public AudioClip rug_sound;

    private void Awake()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
