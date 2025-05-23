using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Score UI")]
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;

    [Header("Game Over UI")]
    public GameObject gameOverWindow;
    public TextMeshProUGUI winnerText;

    void Awake()
    {
        Instance = this;
    }

    public void UpdatePlayer1Score(int score)
    {
        player1ScoreText.text = "Player 1: " + score;
    }

    public void UpdatePlayer2Score(int score)
    {
        player2ScoreText.text = "Player 2: " + score;
    }

    public void ShowGameOverWithWinner(string winnerName)
    {
        winnerText.text = winnerName;
        gameOverWindow.SetActive(true);
    }
}
