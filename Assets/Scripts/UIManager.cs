using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Score UI")]
    [SerializeField] private GameObject scoreUI; // Agrupa los textos de score
    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private TextMeshProUGUI player2ScoreText;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverWindow;
    [SerializeField] private TextMeshProUGUI winnerText;

    [Header("Start/Instruction UI")]
    [SerializeField] private GameObject titleText;
    [SerializeField] private GameObject startInstructionsText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "Start Scene":
                titleText?.SetActive(true);
                startInstructionsText?.SetActive(true);
                scoreUI?.SetActive(false);
                gameOverWindow?.SetActive(false);
                break;

            case "Game":
                scoreUI?.SetActive(true);
                gameOverWindow?.SetActive(false);
                titleText?.SetActive(false);
                startInstructionsText?.SetActive(false);
                break;

            case "End Scene":
                gameOverWindow?.SetActive(true); 
                startInstructionsText?.SetActive(true);
                scoreUI?.SetActive(false);
                titleText?.SetActive(false);
                break;
        }
    }

    public void SetPlayerScore(int playerIndex, int score)
    {
        switch (playerIndex)
        {
            case 0:
                player1ScoreText.text = $"Player 1: {score}";
                break;
            case 1:
                player2ScoreText.text = $"Player 2: {score}";
                break;
        }
    }

    public void ShowGameOver(string winnerName)
    {
        winnerText.text = winnerName;
        gameOverWindow?.SetActive(true);
    }

    public void HideGameOver()
    {
        gameOverWindow?.SetActive(false);
    }
}
