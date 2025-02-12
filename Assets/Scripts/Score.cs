using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static Score Instance; // Singleton for global access

    public int currentScore = 0;
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro UI element

    private void Start()
    {
        currentScore = 0;
    }

    void Awake()
    {
        // Ensure there's only one instance of this script
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate Score managers
        }
    }

    public void IncreaseScore(int amount)
    {
        currentScore += amount;
        UpdateScoreText();
        Debug.Log("Score increased! Current score: " + currentScore);

        // Save the updated score
        ScoreSaver.Instance.UpdateScore(currentScore);
    }


    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}"; // Update TextMeshPro text
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene"); // Replace with your scene name
    }
}
