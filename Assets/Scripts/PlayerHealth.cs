using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 5f;
    public float currentHealth;
    
    public UnityEvent onDeath;
    public SpriteRenderer playerSprite;
    public PlayerController playerController;
    public Light2D playerLight;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            playerLight.enabled = false;
            playerSprite.enabled = false;
            playerController.enabled = false;
            //gameObject.SetActive(false);
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
    
}
