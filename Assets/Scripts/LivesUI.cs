using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TMP_Text LivesTextBox;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    void Update()
    {
        if (gameManager != null)
        {
            LivesTextBox.text = "Lives: " + gameManager.lives.ToString();
        }
    }
}
