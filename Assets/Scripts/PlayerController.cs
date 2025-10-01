using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Reference to the Rigidbody2D component
	private Rigidbody2D rb;

	// Movement speed
	public float moveSpeed = 5f;
	
	// Jump force
	public float jumpForce = 10f;
	
	// Ground check
	public Transform groundCheck;
	public float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	private bool isGrounded;
	
	// Coin throwing
	public GameObject coinPrefab;
	public Transform coinSpawnPoint;
	private bool isFacingRight = true;

	// Start is called before the first frame update
	void Start()
	{
		// Get the Rigidbody2D component
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		// Check if player is grounded
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		
		// Get horizontal input (left/right arrow keys)
		float horizontalInput = 0f;

		if(Input.GetKey(KeyCode.RightArrow))
		{
			horizontalInput = 1f;
			isFacingRight = true;
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		
		// Check for left arrow key
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			horizontalInput = -1f;
			isFacingRight = false;
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		
		// Check for jump input (spacebar)
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			// Apply jump force
			rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
		}
		
		// Check for left mouse button click to throw a coin
		if (Input.GetMouseButtonDown(0) && coinPrefab != null && coinSpawnPoint != null)
		{
			ThrowCoin();
		}

		// Move the player horizontally
		Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
		rb.linearVelocity = movement;
	}
	
	// Draw gizmos for ground check in the editor
	void OnDrawGizmosSelected()
	{
		if (groundCheck != null)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
		}
		
		if (coinSpawnPoint != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(coinSpawnPoint.position, 0.1f);
		}
	}
	
	// Throw a coin in the direction the player is facing
	void ThrowCoin()
	{
		// Instantiate the coin at the spawn point
		GameObject coin = Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity);
		
		// Set the coin's scale to match the player's facing direction
		coin.transform.localScale = new Vector3(
			isFacingRight ? Mathf.Abs(coin.transform.localScale.x) : -Mathf.Abs(coin.transform.localScale.x),
			coin.transform.localScale.y,
			coin.transform.localScale.z
		);
	}
}
