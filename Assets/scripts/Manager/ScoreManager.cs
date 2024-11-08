using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public int score = 0; // Player's score
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro text component



    // Method to add points
    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Method to update the score display
    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}