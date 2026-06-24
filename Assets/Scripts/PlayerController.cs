using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float playerJumpForce = 15.0f;
    [SerializeField] private float playerDodgeForce = 15.0f;
    [SerializeField] private float playerAirControlMultiplier = 0.5f;
    
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
        
        if (_horizontalInput > 0.01f)
            _spriteRenderer.flipX = false;
        else if (_horizontalInput < -0.01f)
            _spriteRenderer.flipX = true;
        
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, playerJumpForce);
        }
    }
    
    private void FixedUpdate()
    {
        var currentSpeed = _isGrounded ? playerSpeed : playerSpeed * playerAirControlMultiplier;
        var targetVelocity = new Vector2(_horizontalInput * currentSpeed, _rb.linearVelocity.y);
        _rb.linearVelocity = targetVelocity;
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