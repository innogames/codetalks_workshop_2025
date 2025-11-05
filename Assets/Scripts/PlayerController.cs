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

	void Update()
	{
		HandleMovement();
		HandleJump();
		HandleAnimation();
	}

	private void HandleMovement()
	{
		moveDirection = move.action.ReadValue<Vector2>();
		rigidbody2d.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rigidbody2d.linearVelocityY);
	}

	private void HandleJump()
	{
		var isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
		if (jump.action.triggered && isGrounded)
		{
			rigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
	}

	private void HandleAnimation()
	{
		animator.SetFloat("Walk", this.moveDirection.x != 0f ? 1f : 0f);
		if (moveDirection.x != 0f)
		{
			transform.localScale = new Vector3(moveDirection.x >= 0 ? 1 : -1, 1, 1);
		}
	}
}