using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // Damage dealt to enemies
    private float destroyYPosition = 5f; // Y position at which the bullet will be destroyed

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
        Debug.Log("Bullet collided with: " + other.gameObject.name);

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy == null)
        {
            // If not found on this object, try to find it in the parent
            enemy = other.GetComponentInParent<Enemy>();
        }

        if (enemy != null)
        {
            Debug.Log("Enemy found, attempting to deal " + damage + " damage");
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No Enemy component found on collided object or its parent");
        }
    }
}
