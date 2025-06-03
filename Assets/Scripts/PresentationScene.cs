using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresentationScene : MonoBehaviour
{
    public GameObject player1;

    private bool player1OnPlatform = false;
    private float timer = 0f;
    private bool countdownStarted = false;
    public float requiredTime = 2f;

    private bool doorOpened = false;
    private ManagePrincipalDoor managePrincipalDoor;

    private AudioManager audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        managePrincipalDoor = GetComponent<ManagePrincipalDoor>();

    }

    private void Update()
    {
        if (player1OnPlatform && !doorOpened)
        {
            if (!countdownStarted)
            {
                countdownStarted = true;
                timer = 0f;
            }

            timer += Time.deltaTime;

            if (timer >= requiredTime)
            {
                StartCoroutine(OpenDoorAndLoadScene());
                doorOpened = true; 
            }
        }
        else
        {
            countdownStarted = false;
            timer = 0f;
        }
    }

    private IEnumerator OpenDoorAndLoadScene()
    {
        managePrincipalDoor.OpenDoor();
        yield return new WaitForSeconds(1.5f); 
        SceneManager.LoadScene("Start Scene");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player1)
        {
            audioManager.PlaySFX(audioManager.rug_sound);
            player1OnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player1)
        {
            player1OnPlatform = false;
        }
    }
}


