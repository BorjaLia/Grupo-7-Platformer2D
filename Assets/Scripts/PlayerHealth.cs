using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5f;
    private float _currentHealth;
    
    public UnityEvent onDeath;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Death();
        }
    }

    public void Heal(float heal)
    {
        if (_currentHealth + heal >= maxHealth)
        {
            _currentHealth = maxHealth;
        }
        else
        {
            _currentHealth += heal;
        }
    }

    private void Death()
    {
        onDeath.Invoke();
    }
    
}
