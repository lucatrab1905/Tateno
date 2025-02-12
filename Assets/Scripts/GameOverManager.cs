using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText; // Reference to the TextMeshPro UI element

    void Start()
    {
        // Retrieve the final score from the Score manager and display it
        if (Score.Instance != null)
        {
            int finalScore = Score.Instance.GetScore();
            finalScoreText.text = $"Final Score: {finalScore}";
        }
        else
        {
            finalScoreText.text = "Final Score: 0";
            Debug.LogError("No Score manager found! Did you forget to add it?");
        }
    }
}
