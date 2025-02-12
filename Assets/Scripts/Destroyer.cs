using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Log when a collision happens
        Debug.Log("Collision detected with: " + other.gameObject.name);

        // Check if the collided object has an "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            // Log that the enemy was detected
            Debug.Log("Enemy detected! Destroying enemy: " + other.gameObject.name);

            // Destroy the enemy GameObject
            Destroy(other.gameObject);
            Destroy(transform.parent.gameObject);


            SceneManager.LoadScene("End");

        }
        else
        {
            // Log if the collided object is not tagged as "Enemy"
            Debug.Log("Collision detected, but object is not an enemy. Object name: " + other.gameObject.name);
        }
    }
}
