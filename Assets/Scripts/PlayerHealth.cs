using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 5f;
    public float currentHealth;
    
    public UnityEvent onDeath;
    private SpriteRenderer _playerSprite;
    private PlayerController _playerController;
    private Light2D _playerLight;
    
    private void Awake()
    {
        _playerSprite = GetComponent<SpriteRenderer>();
        _playerController = GetComponent<PlayerController>();
        _playerLight = GetComponentInChildren<Light2D>(); 
        
        if (_playerSprite == null) Debug.LogError("No Sprite was found for Player");
        if (_playerController == null) Debug.LogError("No Controller Script was found for Player");
        if (_playerLight == null) Debug.LogError("No Light2D found in children of " + gameObject.name);
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Vector2 hitDirection)
    {
        currentHealth -= damage;
        _playerController.DmgBounce(hitDirection);
        
        if (currentHealth <= 0)
        {
            _playerLight.enabled = false;
            _playerSprite.enabled = false;
            _playerController.enabled = false;
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
        onDeath.Invoke();
    }
    public void Respawn()
    {
        currentHealth = maxHealth;
        _playerLight.enabled = true;
        _playerSprite.enabled = true;
        _playerController.enabled = true;
    }
}
