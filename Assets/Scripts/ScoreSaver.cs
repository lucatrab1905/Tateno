using UnityEngine;
using System.IO;

public class ScoreSaver : MonoBehaviour
{
    private string filePath;
    private ScoreData scoreData;

    public static ScoreSaver Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Set the file path to your specified directory
        filePath = Path.Combine(@"C:\Users\lucat\OneDrive\Desktop\äwçZ\Unity\MyGame\Tateno\Assets", "score.json");
        LoadScore();
    }

    public void UpdateScore(int newScore)
    {
        scoreData.score = newScore;
        SaveScore();
        Debug.Log($"Score updated and saved: {newScore}");
    }

    private void SaveScore()
    {
        string json = JsonUtility.ToJson(scoreData);
        File.WriteAllText(filePath, json);
        Debug.Log($"Score saved to: {filePath}");
    }

    private void LoadScore()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            scoreData = JsonUtility.FromJson<ScoreData>(json);
            Debug.Log($"Score loaded: {scoreData.score}");
        }
        else
        {
            scoreData = new ScoreData();
            Debug.Log("No saved score found. Starting with 0.");
        }
    }

    public int GetScore()
    {
        return scoreData.score;
    }

    [System.Serializable]
    private class ScoreData
    {
        public int score = 0;
    }
}
