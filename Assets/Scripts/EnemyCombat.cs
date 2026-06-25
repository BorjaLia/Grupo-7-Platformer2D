using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float enemyDamage = 1;
    [SerializeField] private float attackCooldown = 1f;
    private PlayerHealth _playerHealth;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (_playerHealth != null)
            {
                Vector2 hitDirection = (collision.transform.position - transform.position).normalized;
                _playerHealth.TakeDamage(enemyDamage, hitDirection);
            }
        }
    }
    
   /* private void OnCollisionEnter2D(Collision2D collision) // simple test of health working
    {
        Vector2 hitDirection = (collision.transform.position - transform.position).normalized;
        if (hitDirection.magnitude < 0.1f) 
            hitDirection = Vector2.up;
        collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(enemyDamage, hitDirection);
    }*/
}
