using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private bool _instantDeath = false;


    private EnemyData _runTimeEnemyData;
    private PlayerHealth _playerHealth;
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (enemyData != null)
            _runTimeEnemyData = Instantiate(enemyData);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (_playerHealth != null)
            {
                if (_animator) _animator.SetTrigger("IsAttacking");
                Vector2 hitDirection = (collision.transform.position - transform.position).normalized;
                _playerHealth.TakeDamage(_instantDeath ? _playerHealth.currentHealth : _runTimeEnemyData.damage, hitDirection);
            }
        }
    }
}
