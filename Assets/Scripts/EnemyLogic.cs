using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    
    private Transform _target;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Light2D _enemyLight;
    private Transform _lightPivot; 
    private EnemyData _runTimeEnemyData;
    
    private float _facingDirection = -1;
    private bool _isChasing = false;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
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
        
    }

    private void Update()
    {
        if (_isChasing == true)
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
            _isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _rb.linearVelocity = Vector2.zero;
            _isChasing = false;
        }
    }
}
