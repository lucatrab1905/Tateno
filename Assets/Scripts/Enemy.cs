using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 30;

    public int scoreValue = 10; // Points awarded for destroying

    public void TakeDamage(int damage)
    {
        UnityEngine.Debug.Log("TakeDamage called with damage: " + damage);
        health -= damage;
        UnityEngine.Debug.Log("Enemy health after damage: " + health);
        if (health <= 0)
        {
            UnityEngine.Debug.Log("Enemy health <= 0, destroying enemy");
            Die();
        }
    }

    void Die()
    {
        UnityEngine.Debug.Log("Enemy destroyed!");

        // Check if Score.Instance is null
        if (Score.Instance == null)
        {
            UnityEngine.Debug.LogError("No Score manager found! Make sure a ScoreManager GameObject exists in the scene with the Score script attached.");
            return;
        }

        // Notify the Score manager to increase the score
        UnityEngine.Debug.Log("Increasing score...");
        Score.Instance.IncreaseScore(scoreValue);

        Destroy(gameObject); // Destroy the enemy GameObject
    }
}