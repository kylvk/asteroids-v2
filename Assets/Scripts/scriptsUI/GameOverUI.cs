using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text FinalScoreTextBox, HighScoreTextBox;
    public GameObject Celebrate, GameOverPanel;

    public void ClickRetry()
    {
        Debug.Log("Retry");
        SceneManager.LoadScene("Asteroids");

    }
    public void ClickQuit()
    {
        Debug.Log("Quit");
        SceneManager.LoadScene("Title");
    }

    private Player player;
    private GameManager gameManager;

    public void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    public void Update()
    {
        if (gameManager != null)
        {
            FinalScoreTextBox.text = "Score: " + gameManager.score.ToString();
           // if (highscore < score) 
            HighScoreTextBox.text = "HighScore: " + gameManager.GetHighScore();
        }

    }

}
