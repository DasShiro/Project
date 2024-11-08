using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Button restartButton;

    public bool gameRunning;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
    }
    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public bool IsGameRunning()
    {
        return gameRunning;
    }
    public void GameOver()
    {
        gameRunning = false;
        gameOverPanel.SetActive(true);
    }
}
