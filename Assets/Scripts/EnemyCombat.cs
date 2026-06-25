using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float enemyDamage = 1;

    private void OnCollisionEnter2D(Collision2D collision) // simple test of health working
    {
        collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(enemyDamage);
    }
}
