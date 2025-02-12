using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

public class Access : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;

    private float timer = 0.0f;
    private float interval = 1.0f; // Save every 1 second

    private Score scoreScript; // Reference to the Score script

    void Start()
    {
        try
        {
            // Connect to MongoDB
            string connectionString = "mongodb+srv://game22403036:password69@cluster0.ku7ll.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            client = new MongoClient(connectionString);

            database = client.GetDatabase("Score");
            collection = database.GetCollection<BsonDocument>("score");

            // Find and reference the Score script
            scoreScript = FindObjectOfType<Score>();
            if (scoreScript == null)
            {
                Debug.LogError("No Score script found in the scene!");
                return;
            }

            //LoadScore();
        }
        catch (Exception e)
        {
            Debug.LogError($"MongoDB Error: {e.Message}");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            SaveScore();
            timer = 0.0f;
        }
    }

    private void LoadScore()
    {
        try
        {
            var document = collection.Find(new BsonDocument()).FirstOrDefault();
            if (document != null && document.Contains("score"))
            {
                int savedScore = document["score"].AsInt32;

                // Update the score in the Score script
                if (scoreScript != null)
                {
                    scoreScript.currentScore = savedScore;
                    Debug.Log($"Score loaded and updated in Score script: {savedScore}");
                }
            }
            else
            {
                Debug.Log("No score found in database. Starting at 0.");
                if (scoreScript != null)
                {
                    scoreScript.currentScore = 0; // Default score if no document exists
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading score: {e.Message}");
        }
    }

    private void SaveScore()
    {
        try
        {
            if (scoreScript == null) return;

            // Get the current score from the Score script
            int currentScore = scoreScript.GetScore();

            // Save the score to MongoDB
            var filter = new BsonDocument();
            var update = Builders<BsonDocument>.Update.Set("score", currentScore);
            var options = new UpdateOptions { IsUpsert = true };

            collection.UpdateOne(filter, update, options);
            Debug.Log($"Score saved to database: {currentScore}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving score: {e.Message}");
        }
    }
}
