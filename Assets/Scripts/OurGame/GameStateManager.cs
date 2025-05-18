using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameObject player1;
    public GameObject player2;

    private int player1Score = 0;
    private int player2Score = 0;

    public float gameDuration = 60f; // Duraci�n del juego en segundos
    private float timer;

    private bool gameEnded = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        timer = gameDuration;
    }

    void Update()
    {
        if (gameEnded) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void AddScore(GameObject player, int amount)
    {
        if (player == player1)
        {
            player1Score += amount;
            UIManager.Instance.UpdatePlayer1Score(player1Score);
        }
        else if (player == player2)
        {
            player2Score += amount;
            UIManager.Instance.UpdatePlayer2Score(player2Score);
        }
    }

    private void EndGame()
    {
        gameEnded = true;

        string winnerName = "Tie!";
        if (player1Score > player2Score)
        {
            winnerName = player1.name + " Wins!";
        }
        else if (player2Score > player1Score)
        {
            winnerName = player2.name + " Wins!";
        }

        UIManager.Instance.ShowGameOverWithWinner(winnerName);
    }

    // Opcional: Accesores p�blicos
    public int GetPlayer1Score() => player1Score;
    public int GetPlayer2Score() => player2Score;
}
