using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float playerSpeed = 4.0f;
    [SerializeField] private float playerJumpForce = 15.0f;
    [SerializeField] private float playerDodgeForce = 15.0f;
    [SerializeField] private float playerAirControlMultiplier = 0.5f;
    
    [Header("Player Animator")]
    [SerializeField] private Animator animator;
    
    [Header("References")]
    [SerializeField] private Transform lightPivot; 
    
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded;
    private float _horizontalInput;
    private bool _canMove;

    private void Awake()
    {
        _canMove = true;
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody2D>();
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        { 
            Debug.LogWarning("No SpriteRenderer found on the player.");
        }
        
        PlayerHealth playerHealth = GetComponent<PlayerHealth>(); 
        if (playerHealth != null)
        {
            playerHealth.onDeath.AddListener(HandleDeath);
        }
    }
    private void Update()
    {
        if (!_canMove) return;
        
        _horizontalInput = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, playerJumpForce);
        }
        
        animator.SetFloat("Speed", Mathf.Abs(_rb.linearVelocity.x));
        
        UpdateFacingDirection();
        GetMouseWorldPosition();
    }
    
    private void FixedUpdate()
    {
        if (!_canMove) return;
        
        var currentSpeed = _isGrounded ? playerSpeed : playerSpeed * playerAirControlMultiplier;
        var targetVelocity = new Vector2(_horizontalInput * currentSpeed, _rb.linearVelocity.y);
        _rb.linearVelocity = targetVelocity;
    }
    private Vector3 GetMouseWorldPosition() // gets rid of camera frustum error
    {
        var mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z; 
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void HandleDeath()
    {
        _canMove = false;
        _rb.linearVelocity = Vector2.zero;
        Debug.Log("Player Died");
    }
    private void UpdateFacingDirection() // Function That is responsible for switching the sprite and if the character has a light sprite, switch it too 
    {
        if (_horizontalInput > 0.01f)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_horizontalInput < -0.01f)
        {
            _spriteRenderer.flipX = true;
        }
        
        if (lightPivot == null) return;
        var facingDirection = _spriteRenderer.flipX ? -1f : 1f;
        lightPivot.localScale = new Vector3(facingDirection, 1f, 1f);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        { 
            _isGrounded = true;
        }
    } 
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}