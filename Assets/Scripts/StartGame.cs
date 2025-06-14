using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private bool player1OnPlatform = false;
    private bool player2OnPlatform = false;
    private float timer = 0f;
    private bool countdownStarted = false;
    public float requiredTime = 2f;

    private bool doorClosed = false;
    private ManagePrincipalDoor managePrincipalDoor;

    public DirectionalLightController lightController; 


    private void Start()
    {
        managePrincipalDoor = GetComponent<ManagePrincipalDoor>();

    }

    private void Update()
    {
        if (player1OnPlatform && player2OnPlatform && !doorClosed)
        {
            if (!countdownStarted)
            {
                countdownStarted = true;
                timer = 0f;
            }

            timer += Time.deltaTime;

            if (timer >= requiredTime)
            {
                StartCoroutine(CloseDoorAndLoadScene());
                doorClosed = true;
            }
        }
        else
        {
            countdownStarted = false;
            timer = 0f;
        }
    }

    private IEnumerator CloseDoorAndLoadScene()
    {
        managePrincipalDoor.CloseDoor();
        lightController.TurnOffLight();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player1)
        {
            player1OnPlatform = true;
        }
        else if (other.gameObject == player2)
        {
            player2OnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player1)
        {
            player1OnPlatform = false;
        }
        else if (other.gameObject == player2)
        {
            player2OnPlatform = false;
        }
    }
}


