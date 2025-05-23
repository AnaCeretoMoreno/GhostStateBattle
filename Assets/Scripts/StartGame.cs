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

    private void Update()
    {
        if (player1OnPlatform && player2OnPlatform)
        {
            //Debug.Log("Both players inside");
            if (!countdownStarted)
            {
                //Debug.Log("Start countdown");
                countdownStarted = true;
                timer = 0f;
            }

            timer += Time.deltaTime;
            //Debug.Log(timer);

            if (timer >= requiredTime)
            {
                SceneManager.LoadScene("Game");
            }
        }
        else
        {
            countdownStarted = false;
            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter is " + other.gameObject.name);
        if (other.gameObject == player1)
        {
            //Debug.Log("Player 1 enters");
            player1OnPlatform = true;
        }
        else if (other.gameObject == player2)
        {
            //Debug.Log("Player 2 enters");
            player2OnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player1)
        {
            //Debug.Log("Player 1 exits");
            player1OnPlatform = false;
        }
        else if (other.gameObject == player2)
        {
            //Debug.Log("Player 2 exits");
            player2OnPlatform = false;
        }
    }
}


