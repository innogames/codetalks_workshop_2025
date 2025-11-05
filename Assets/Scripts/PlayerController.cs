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

	private void HandleMovement()
	{
		this.moveDirection = this.move.action.ReadValue<Vector2>();
		this.rigidbody2d.linearVelocity = new Vector2(
			this.moveDirection.x * this.moveSpeed, 
			this.rigidbody2d.linearVelocity.y
		);
	}
	
	void Update()
	{
		this.HandleMovement();
		this.HandleJump();
		this.HandleAnimation();
	}

	private void HandleJump()
	{
		var ground = Physics2D.OverlapCircle(
			this.transform.position, 
			this.groundCheckRadius, 
			this.groundLayer
		);
		if (ground is not null && this.jump.action.triggered)
		{
			this.rigidbody2d.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
		}
	}

	private void HandleAnimation()
	{
		this.animator.SetFloat("Walk", this.moveDirection.x != 0f ? 1f : 0f);

		if (this.moveDirection.x != 0f)
		{
			this.transform.localScale = new Vector3(
				this.moveDirection.x >= 0f ? 1f : -1f,
				1f,
				1f
			);
		}
	}
}