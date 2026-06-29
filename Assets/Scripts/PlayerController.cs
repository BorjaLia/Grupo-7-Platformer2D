using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] private PlayerData movementData; 
    [Header("Player Animator")]
    [SerializeField] private Animator animator;
    
    private PlayerData _runTimeMovementData;
    
    private Rigidbody2D _rb;
    private Transform _groundCheckPoint;
    private SpriteRenderer _spriteRenderer;
    private Light2D _playerLight;
    private Transform _lightPivot; 
    private PlayerCombat _combat;
    private bool _isGrounded;
    private float _horizontalInput;
    private bool _canMove;
    private bool _isHurt = false;
    private bool _isPaused = false;
    
    private void Awake()
    {
        GameManager.OnPauseToggled += OnPause;

        if (movementData != null)
            _runTimeMovementData = Instantiate(movementData);
        else
            Debug.LogError("Player Movement Data is missing!");
        
        _canMove = true;
        _playerLight = GetComponentInChildren<Light2D>();
        _lightPivot = _playerLight.transform;
        _rb = GetComponent<Rigidbody2D>();
        _combat = GetComponent<PlayerCombat>();
        
        if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody2D>();
        }
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        { 
            Debug.LogWarning("No SpriteRenderer found on the player.");
        }
        _groundCheckPoint = transform.Find("GroundCheckPoint");
        if (_groundCheckPoint == null)
        {
            Debug.LogWarning("GroundCheckPoint child not found! Please create an empty child named GroundCheckPoint.");
        }
        PlayerHealth playerHealth = GetComponent<PlayerHealth>(); 
        if (playerHealth != null)
        {
            playerHealth.onDeath.AddListener(HandleDeath);
        }
        if (_playerLight == null) Debug.LogError("No Light2D found in children of " + gameObject.name);
        if (_combat == null) Debug.LogError("No PlayerCombat Script found on the player.");
    }
    private void Update()
    {
        if (!_canMove || _isPaused) return;
        
        _horizontalInput = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _runTimeMovementData.playerJumpForce);
        }
        
        animator.SetFloat("Speed", Mathf.Abs(_rb.linearVelocity.x));
        
        UpdateFacingDirection();
        GetMouseWorldPosition();
    }
    
    private void FixedUpdate()
    {
        if (!_canMove || _isHurt || _isPaused) return;
        
        _isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _runTimeMovementData.groundCheckRadius, _runTimeMovementData.groundLayerMask);
        
        var currentSpeed = _isGrounded ? _runTimeMovementData.playerSpeed : _runTimeMovementData.playerSpeed * _runTimeMovementData.playerAirControlMultiplier;
        var targetVelocity = new Vector2(_horizontalInput * currentSpeed, _rb.linearVelocity.y);
        _rb.linearVelocity = targetVelocity;
    }

    private void OnDestroy()
    {
        GameManager.OnPauseToggled -= OnPause;
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
        
        if (_lightPivot == null) return;
        var facingDirection = _spriteRenderer.flipX ? -1f : 1f;
        _lightPivot.localScale = new Vector3(facingDirection, 1f, 1f);
    }

    public void DmgBounce(Vector2 direction)
    {
        _rb.linearVelocity = new Vector2(direction.x * _runTimeMovementData.playerHurtForce, _rb.linearVelocity.y);
        _isHurt = true;
        Invoke(nameof(ResetHurt), _runTimeMovementData.hurtTimer);
    }
    private void ResetHurt()
    {
        _isHurt = false;
    }
    
    private void OnDrawGizmosSelected()
    {
        if (_groundCheckPoint == null) return;
        
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(_groundCheckPoint.position, _runTimeMovementData.groundCheckRadius);
    }

    private void OnPause(bool pause)
    {
        _isPaused = pause;
    }
}