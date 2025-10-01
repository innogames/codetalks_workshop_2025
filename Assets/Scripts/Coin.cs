using UnityEngine;

public class Coin : MonoBehaviour
{
    // Speed at which the coin moves
    public float speed = 8f;
    
    // How long the coin exists before being destroyed
    public float lifetime = 3f;
    
    // Direction the coin will travel
    private Vector2 direction;
    
    // Reference to the Rigidbody2D component
    private Rigidbody2D rb;
    
    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        
        // Set the direction to right (forward)
        direction = Vector2.right;
        
        // If the player is facing left, flip the direction
        if (transform.localScale.x < 0)
        {
            direction = Vector2.left;
        }
        
        // Apply initial velocity
        rb.linearVelocity = direction * speed;
        
        // Destroy the coin after lifetime seconds
        Destroy(gameObject, lifetime);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the coin hit an enemy
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            // Destroy the enemy
            Destroy(collision.gameObject);
            
            // Optional: Add particle effect or sound here
        }
        
        // Destroy the coin regardless of what it hit
        Destroy(gameObject);
    }
}
