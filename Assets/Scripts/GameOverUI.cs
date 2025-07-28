using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;

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

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    void Update()
    {
        if (gameManager != null)
        {
            FinalScoreTextBox.text = "Score: " + gameManager.score.ToString();
            HighScoreTextBox.text = "HighScore: " + gameManager.score.ToString();
        }

    }

}
