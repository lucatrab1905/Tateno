using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCon : MonoBehaviour
{
    public float fallSpeed = 1f; // Speed at which the enemy falls
    public float horizontalSpeed = 2f; // Speed of horizontal movement
    public float horizontalDistance = 2f; // Distance of horizontal movement

    private Vector3 startPosition;
    private float sinWaveTime = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Move downward
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Horizontal sine wave movement
        sinWaveTime += Time.deltaTime;
        float horizontalOffset = Mathf.Sin(sinWaveTime * horizontalSpeed) * horizontalDistance;

        Vector3 newPosition = transform.position;
        newPosition.x = startPosition.x + horizontalOffset;
        transform.position = newPosition;

        // Optional: Destroy the enemy if it falls below a certain y position
        if (transform.position.y < -10f) // Adjust this value as needed
        {
            Destroy(gameObject);
        }
    }
}