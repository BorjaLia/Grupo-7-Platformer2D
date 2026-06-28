using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] private PlayerData healthData;
    private PlayerData _runTimeHealthData;
    
    public float maxHealth; 
    public float currentHealth;
    public UnityEvent onDeath;
    
    private SpriteRenderer _playerSprite;
    private PlayerController _playerController;
    private Light2D _playerLight;
    private Animator _animator;
    
    private void Awake()
    {
        _playerSprite = GetComponent<SpriteRenderer>();
        _playerController = GetComponent<PlayerController>();
        _playerLight = GetComponentInChildren<Light2D>();
        _animator = GetComponent<Animator>();
        
        if (healthData != null)
            _runTimeHealthData = Instantiate(healthData);
        
        CheckMissingElements();
        
        maxHealth = _runTimeHealthData.maxHealth;
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Vector2 hitDirection)
    {
        currentHealth -= damage;
        if (currentHealth > 0)
        {
            _animator.SetTrigger("IsHurt");
        }
        _playerController.DmgBounce(hitDirection);
        
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Heal(float heal)
    {
        if (currentHealth + heal >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += heal;
        }
    }

    private void Death()
    {
        _playerLight.enabled = false;
        _playerSprite.enabled = false;
        _playerController.enabled = false;
        onDeath.Invoke();
    }
    public void Respawn()
    {
        currentHealth = maxHealth;
        _playerLight.enabled = true;
        _playerSprite.enabled = true;
        _playerController.enabled = true;
    }

    private void CheckMissingElements()
    {
        if (healthData == null)
            Debug.LogError("Player Health Data is missing!");
        
        if (_playerSprite == null)
            Debug.LogError("No Sprite was found for Player");
        
        if (_playerController == null)
            Debug.LogError("No Controller Script was found for Player");
        
        if (_playerLight == null) 
            Debug.LogError("No Light2D found in children of " + gameObject.name);
        
        if (_animator == null)
            Debug.LogError("Player Animator is missing!");
    }
}


      
