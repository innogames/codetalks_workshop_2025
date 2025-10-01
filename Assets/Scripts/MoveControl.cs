using UnityEngine;
using UnityEngine.InputSystem;

public class MoveControl : MonoBehaviour
{
	public InputActionReference move;
	public InputActionReference jump;

	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private float jumpForce = 10f;
	[SerializeField] private LayerMask groundLayer = 1;
	[SerializeField] private LayerMask wallLayer = 1;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private float groundCheckRadius = 0.2f;
	[SerializeField] private float wallCheckDistance = 0.6f;

	private Rigidbody2D rb;
	private Animator animator;
	private Vector2 moveDirection;
	private bool facingRight = true;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		if (GetComponent<Collider2D>() == null)
		{
			gameObject.AddComponent<BoxCollider2D>();
		}
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
		rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

		if (animator != null)
			animator.SetFloat("Walk", Mathf.Abs(moveX));

		if (moveX > 0 && !facingRight)
			Flip();
		else if (moveX < 0 && facingRight)
			Flip();
	}

	private void Flip()
	{
		facingRight = !facingRight;
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}

	private void Jump()
	{
		bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		if (isGrounded)
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
	}

	private void OnEnable()
	{
		jump.action.started += ctx => Jump();
	}

	private void OnDisable()
	{
		jump.action.started -= ctx => Jump();
	}

	void OnDrawGizmosSelected()
	{
		if (groundCheck != null)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
		}

		Gizmos.color = Color.red;
		Vector3 wallCheckPos = transform.position + Vector3.right * wallCheckDistance;
		Gizmos.DrawLine(transform.position, wallCheckPos);
		wallCheckPos = transform.position + Vector3.left * wallCheckDistance;
		Gizmos.DrawLine(transform.position, wallCheckPos);
	}
}