using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text ScoreTextBox;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    void Update()
    {
        if (gameManager != null)
        {
            ScoreTextBox.text = "Credits: " + gameManager.score.ToString();
        }
    }
}
