using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth; 
    public float currentHealth;
    public UnityEvent onDeath;
    
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private EnemyHealthbarBehaviour healthBar;
    private EnemyLogic _enemyLogic;
    private EnemyData _runTimeEnemyData;
    private Animator _animator;
    

    private void Awake()
    {
        if (enemyData != null)
            _runTimeEnemyData = Instantiate(enemyData);
        
        _animator = GetComponent<Animator>();
        _enemyLogic = GetComponent<EnemyLogic>();
        
        maxHealth = _runTimeEnemyData.maxHealth;
        currentHealth = maxHealth;
    }

    private void Start()
    {
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth > 0)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
           _animator.SetTrigger("IsHurt");
        } 
        
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
