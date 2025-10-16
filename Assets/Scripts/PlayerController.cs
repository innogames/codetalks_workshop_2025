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
	}
}