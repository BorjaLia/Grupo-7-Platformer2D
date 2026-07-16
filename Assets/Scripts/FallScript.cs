using UnityEngine;

public class FallScript : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    
    private PlayerData _runTimePlayerData;
    private PlayerHealth _playerHealth;


    private void Awake()
    {
        if (playerData != null)
            _runTimePlayerData = Instantiate(playerData);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (_playerHealth != null)
            {
                Vector2 hitDirection = (collision.transform.position - transform.position).normalized;
                _playerHealth.TakeFallDamage(_runTimePlayerData.maxHealth);
            }
        }
    }
}
