using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float chargeSpeed;
    public GameObject bulletPrefab;
    public GameObject chargedShotPrefab;
    public float bulletSpeed = 10f;
    public float chargeTime = 1f;
    public float bulletSpawnOffset = 0.5f;

    private float currentSpeed;
    private bool isCharging = false;
    private float chargeStartTime;
    private Rigidbody2D rb;

    void Start()
    {
        currentSpeed = normalSpeed;
        chargeSpeed = normalSpeed / 2f;
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * currentSpeed;
        rb.velocity = movement;
    }

    void Update()
    {
        // Shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            chargeStartTime = Time.time;
            isCharging = true;
            currentSpeed = chargeSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Time.time - chargeStartTime >= chargeTime)
            {
                FireChargedShot();
            }
            else
            {
                FireNormalShot();
            }
            isCharging = false;
            currentSpeed = normalSpeed;
        }
    }

    void FireNormalShot()
    {
        Vector2 spawnPosition = rb.position + Vector2.up * bulletSpawnOffset;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.Euler(0, 0, 90));
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = Vector2.up * bulletSpeed;
    }

    void FireChargedShot()
    {
        Vector2 spawnPosition = rb.position + Vector2.up * bulletSpawnOffset;
        GameObject chargedShot = Instantiate(chargedShotPrefab, spawnPosition, Quaternion.Euler(0, 0, 90));
        Rigidbody2D chargedShotRb = chargedShot.GetComponent<Rigidbody2D>();
        chargedShotRb.velocity = Vector2.up * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Log the collision for debugging
        Debug.Log("Collision detected with: " + other.gameObject.name);

        // Check if the collided object is tagged as "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Destroy the enemy GameObject
            Debug.Log("Enemy detected! Destroying enemy: " + other.gameObject.name);
            Destroy(other.gameObject);

            // Destroy the player GameObject (this GameObject)
            Debug.Log("Destroying player!");
            Destroy(gameObject);
        }
        else
        {
            // Log if collision is not with an enemy
            Debug.Log("Collision detected but not with an enemy.");
        }
    }


}
