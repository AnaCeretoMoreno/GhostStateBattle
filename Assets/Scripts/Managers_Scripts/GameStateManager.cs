using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    private int player1Score = 0;
    private int player2Score = 0;

    public int winScore = 20;

    public GameObject player1;
    public GameObject player2;

    private bool gameEnded = false;
    public ManagePrincipalDoor managePrincipalDoor;

    AudioManager audioManager;

    private bool doorOpened = false;


    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (managePrincipalDoor == null)
            Debug.LogError("ManagePrincipalDoor not found in the scene!");
    }

    void Update()
    {
        if (gameEnded) return;

        if (Input.GetKeyDown(KeyCode.Escape) && !doorOpened)
        {
            StartCoroutine(OpenDoorAndLoadSceneStart());
            doorOpened = true;
        }
    }

    private IEnumerator OpenDoorAndLoadSceneStart()
    {
        managePrincipalDoor.OpenDoor();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Start Scene");
    }

    public void AddScore(GameObject player, int amount)
    {
        if (gameEnded || player == null) return;


        if (player != null)
        {
            Debug.LogWarning(player.tag);

            if (player.tag == "Player1")
            {
                player1Score += amount;
                UIManager.Instance.SetPlayerScore(0, player1Score);

                if (player1Score >= winScore)
                {
                    EndGameImmediate("Player 1 Wins the House!");
                    return;
                }
            } 
            else if (player.tag == "Player2")
            {
                player2Score += amount;
                UIManager.Instance.SetPlayerScore(1, player2Score);

                if (player2Score >= winScore)
                {
                    EndGameImmediate("Player 2 Wins the House!");
                    return;
                }
            } 
            else
            {
                Debug.LogWarning("Object" + player.name + " has no valid tag (Player1 or Player2).");
            }
        }
    }


    private void EndGameImmediate(string winnerName)
    {
        gameEnded = true;

        PlayerPrefs.SetString("WinnerName", winnerName); // Guardamos el ganador
        PlayerPrefs.Save();

        if (!doorOpened)
        {
            StartCoroutine(OpenDoorAndLoadSceneEnd());
            doorOpened = true;
        }

    }

    private IEnumerator OpenDoorAndLoadSceneEnd()
    {
        managePrincipalDoor.OpenDoor();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("End Scene");
    }


    public int GetPlayer1Score() => player1Score;
    public int GetPlayer2Score() => player2Score;
}
