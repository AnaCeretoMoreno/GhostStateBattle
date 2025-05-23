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

    public float gameDuration = 60f; // Duración del juego en segundos
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
    Debug.Log("AddScore llamado. Jugador: " + player.name + " | Puntos: " + amount);

    if (player == player1)
    {
        player1Score += amount;
        Debug.Log("¡Puntos sumados a Player1! Total: " + player1Score);
        UIManager.Instance.UpdatePlayer1Score(player1Score);
    }
    else if (player == player2)
    {
        player2Score += amount;
        Debug.Log("¡Puntos sumados a Player2! Total: " + player2Score);
        UIManager.Instance.UpdatePlayer2Score(player2Score);
    }
    else
    {
        Debug.LogWarning("El jugador " + player.name + " no está asignado como player1 ni player2.");
    }


    /*    Debug.Log("AddScore llamado. Jugador: " + player.name + " | Puntos: " + amount);

    if (player == player1)
    {
        player1Score += amount;
        Debug.Log("¡Puntos sumados a Player1! Total: " + player1Score);
        UIManager.Instance.UpdatePlayer1Score(player1Score);
    }
    else if (player == player2)
    {
        player2Score += amount;
        Debug.Log("¡Puntos sumados a Player2! Total: " + player2Score);
        UIManager.Instance.UpdatePlayer2Score(player2Score);
    }
    else
    {
        Debug.LogWarning("El jugador " + player.name + " no está asignado como player1 ni player2.");
    }
        if (player == player1)
        {
            player1Score += amount;
            UIManager.Instance.UpdatePlayer1Score(player1Score);
        }
        else if (player == player2)
        {
            player2Score += amount;
            UIManager.Instance.UpdatePlayer2Score(player2Score);
        }*/
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

    // Opcional: Accesores públicos
    public int GetPlayer1Score() => player1Score;
    public int GetPlayer2Score() => player2Score;
}
