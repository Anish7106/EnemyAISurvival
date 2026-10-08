using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;

    void Start()
    {
        UpdateScore();
        gameOverPanel.SetActive(false);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "SCORE: " + score;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}