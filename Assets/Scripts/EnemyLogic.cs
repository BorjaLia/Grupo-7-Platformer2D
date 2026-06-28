using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyLogic : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Light2D _enemyLight;
    private Transform _lightPivot; 
    
    private float _horizontalInput;
    
    private void Awake()
    {
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

    private void Update()
    {
        UpdateFacingDirection();
    }

    public void DmgBounce(Vector2 direction)
    {
        
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
}
