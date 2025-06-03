using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("-------Audio Sources------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("-------Music Clips------")]
    public AudioClip gameplayMusic;
    public AudioClip titleMusic;
    public AudioClip houseMusic;

    [Header("-------Sound Effects------")]
    public AudioClip normal_death;
    public AudioClip special_death;
    public AudioClip normal_spawn;
    public AudioClip special_spawn;
    public AudioClip penalize_sound;
    public AudioClip player_wins;
    public AudioClip rug_sound;
    public AudioClip doorOpen;
    public AudioClip doorClose;


    private string currentScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Game")
        {
            if (gameplayMusic != null)
            {
                musicSource.clip = gameplayMusic;
                musicSource.Play();
            }
        } else if (currentScene == "Presentation")
        {
            if (titleMusic != null)
            {
                musicSource.clip = titleMusic;
                musicSource.Play();
            }
        }
        else
        {
            if (houseMusic != null)
            {
                musicSource.clip = houseMusic;
                musicSource.Play();
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        if (currentScene == "Game" && clip == rug_sound) return;

        if (currentScene != "Game" && clip != rug_sound && clip != doorClose && clip != doorOpen) return;

        SFXSource.PlayOneShot(clip);
    }
}
