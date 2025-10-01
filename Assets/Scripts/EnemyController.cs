using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Movement speed
    public float moveSpeed = 3f;
    
    // Direction (1 = right, -1 = left)
    public float direction = 1f;
    
    // Reference to the Rigidbody2D component
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        
        // Set initial scale based on direction
        UpdateFacingDirection();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Move the enemy in the specified direction
        Vector2 movement = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = movement;
    }
    
    // Update the sprite's facing direction based on movement direction
    void UpdateFacingDirection()
    {
        // If direction is positive (moving right), make sure scale.x is positive
        // If direction is negative (moving left), make sure scale.x is negative
        transform.localScale = new Vector3(
            direction > 0 ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
        );
    }
    
    // Called when the enemy collides with something
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is on the left or right side
        ContactPoint2D contact = collision.GetContact(0);
        float dotProduct = Vector2.Dot(contact.normal, Vector2.right);
        
        // If the collision is on the left or right side (dot product close to 1 or -1)
        if (Mathf.Abs(dotProduct) > 0.5f)
        {
            // Reverse direction
            direction *= -1;
            
            // Update facing direction
            UpdateFacingDirection();
        }
    }
}
