using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    
    private Transform _target;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Light2D _enemyLight;
    private Transform _lightPivot; 
    private EnemyData _runTimeEnemyData;
    private Animator _animator;
    private EnemyState _currentState;
    
    private float _facingDirection = -1;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        if (enemyData != null)
            _runTimeEnemyData = Instantiate(enemyData);
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        { 
            Debug.LogWarning("No SpriteRenderer found on the player.");
        }
        
        _enemyLight = GetComponentInChildren<Light2D>();
        if (_enemyLight == null)
        {
            Debug.LogError("No Light2D found in children of " + gameObject.name);
        }
        _lightPivot = _enemyLight.transform;
    }

    private void Start()
    {
        ChangeState(_currentState = EnemyState.idle);
    }

    private void Update()
    {
        if (_currentState == EnemyState.chasing)
        {
            if (_target.position.x > transform.position.x && _facingDirection == -1 ||
                _target.position.x < transform.position.x && _facingDirection == 1)
            {
                UpdateFacingDirection();
            }
            Vector2 direction = (_target.position - transform.position).normalized;
            _rb.linearVelocity = direction * _runTimeEnemyData.speed;
        }
    }

    public void DmgBounce(Vector2 direction)
    {
        
    }
    
    private void UpdateFacingDirection() // Function That is responsible for switching the sprite and if the character has a light sprite, switch it too 
    {
        _facingDirection *= -1;
        
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        if (_lightPivot == null) return;
        var facingDirection = _spriteRenderer.flipX ? -1f : 1f;
        _lightPivot.localScale = new Vector3(facingDirection, 1f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (_target == null)
            {
                _target = collision.transform;
            }
            ChangeState(EnemyState.chasing);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.idle);
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (_currentState == EnemyState.idle)
        {
            _animator.SetBool("IsIdle", false);
        }
        else if (_currentState == EnemyState.chasing)
        {
            _animator.SetBool("IsChasing", false);
        }
        
        _currentState = newState;
        
        if (_currentState == EnemyState.idle)
        {
            _animator.SetBool("IsIdle", true);
        }
        else if (_currentState == EnemyState.chasing)
        {
            _animator.SetBool("IsChasing", true);
        }
    }

    public enum EnemyState
    {
        idle = 0,
        chasing = 1,
    }
}
