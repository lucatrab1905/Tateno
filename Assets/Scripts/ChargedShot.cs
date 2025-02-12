using System.Diagnostics;
using UnityEngine;

public class ChargedShot : MonoBehaviour
{
    public int damage = 30;
    public float explosionRadius = 2f;
    private float destroyYPosition = 5f;

    void Update()
    {
        // Check if the bullet has gone beyond the destroy position
        if (transform.position.y > destroyYPosition)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log("Bullet collided with: " + other.gameObject.name);
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null)
        {
            // If not found on this object, try to find it in the parent
            enemy = other.GetComponentInParent<Enemy>();
        }

        if (enemy != null)
        {
            UnityEngine.Debug.Log("Enemy found, attempting to deal " + damage + " damage");
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            UnityEngine.Debug.Log("No Enemy component found on collided object or its parent");
        }
    }
}