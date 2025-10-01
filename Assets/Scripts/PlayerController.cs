using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Input Actions")]
	[SerializeField]
	private InputActionReference move;
	
	[SerializeField]
	public InputActionReference jump;
	
	[Header("General References")]
	[SerializeField]
	private Animator animator;

	[SerializeField]
	private Rigidbody2D rigidbody2d;

	[Header("Movement Settings")]
	[SerializeField] 
	private float moveSpeed = 5f;
	
	[SerializeField] 
	private float jumpForce = 10f;
	
	[Header("Collision Settings")]
	[SerializeField] 
	private LayerMask groundLayer;
	
	[SerializeField] 
	private LayerMask wallLayer;
	
	[SerializeField] 
	private float groundCheckRadius = 0.2f;
	
	[SerializeField] 
	private float wallCheckDistance = 0.6f;

	private Vector2 moveDirection;
	private bool facingRight = true;

	void Start()
	{
	}

	void Update()
	{
		moveDirection = move.action.ReadValue<Vector2>();
		HandleMovement();
	}

	private void HandleMovement()
	{
		bool hitWall = Physics2D.Raycast(transform.position, Vector2.right * moveDirection.x, wallCheckDistance,
			wallLayer);

		float moveX = hitWall ? 0f : moveDirection.x;
		rigidbody2d.linearVelocity = new Vector2(moveX * moveSpeed, rigidbody2d.linearVelocity.y);
		animator.SetFloat("Walk", Mathf.Abs(moveX));

		if (moveX > 0 && !facingRight || moveX < 0 && facingRight)
		{
			Flip();
		}
	}

	private void Flip()
	{
		facingRight = !facingRight;
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}

	private void Jump()
	{
		bool isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
		if (isGrounded)
		{
			rigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
	}

	private void OnEnable()
	{
		jump.action.started += _ => Jump();
	}

	private void OnDisable()
	{
		jump.action.started -= _ => Jump();
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, groundCheckRadius);

		Gizmos.color = Color.red;
		Vector3 wallCheckPos = transform.position + Vector3.right * wallCheckDistance;
		Gizmos.DrawLine(transform.position, wallCheckPos);
		wallCheckPos = transform.position + Vector3.left * wallCheckDistance;
		Gizmos.DrawLine(transform.position, wallCheckPos);
	}
}