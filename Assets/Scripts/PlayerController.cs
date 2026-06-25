using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float lives = 5.0f;
    [SerializeField] private float playerAttackForce = 10.0f;
    
    [Header("Player Movement Settings")]
    [SerializeField] private float playerSpeed = 5.0f;
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

    private void Awake()
    {
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
    }
    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, playerJumpForce);
        }
        
        animator.SetFloat("Speed", Mathf.Abs(_rb.linearVelocity.x));
        
        UpdateFacingDirection(); 
    }
    
    private void FixedUpdate()
    {
        var currentSpeed = _isGrounded ? playerSpeed : playerSpeed * playerAirControlMultiplier;
        var targetVelocity = new Vector2(_horizontalInput * currentSpeed, _rb.linearVelocity.y);
        _rb.linearVelocity = targetVelocity;
    }

    
    private void UpdateFacingDirection() // Function That is responsible for switching the sprite and if the character has one the illumination of it 
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