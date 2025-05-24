using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    private int player1Score = 0;
    private int player2Score = 0;

    public int winScore = 30;

    public GameObject player1;
    public GameObject player2;

    private bool gameEnded = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (gameEnded) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start Scene");
        }
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
                    EndGameImmediate("Player 1 Wins!");
                    return;
                }
            } 
            else if (player.tag == "Player2")
            {
                player2Score += amount;
                UIManager.Instance.SetPlayerScore(1, player2Score);

                if (player2Score >= winScore)
                {
                    EndGameImmediate("Player 2 Wins!");
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
        UIManager.Instance.ShowGameOver(winnerName);
        StartCoroutine(ReturnToEndScene());
    }

    private IEnumerator ReturnToEndScene()
    {
        yield return new WaitForSeconds(10f); // Espera 10 segundos
        SceneManager.LoadScene("End Scene");
    }

    public int GetPlayer1Score() => player1Score;
    public int GetPlayer2Score() => player2Score;
}
