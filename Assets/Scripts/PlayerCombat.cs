using System;
using System.Linq;
using UnityEngine;
public class PlayerCombat : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] private PlayerData combatData; 
     private Animator _animator;
     private PlayerData _runTimeCombatData;
     private Transform _attackPoint;
     private float _timer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (combatData != null)
            _runTimeCombatData = Instantiate(combatData);
        else
            Debug.LogError("Player Combat Data is missing!");
        _attackPoint = transform.Find("AttackPoint");
        if (_attackPoint == null)
        {
            Debug.LogWarning("AttackPoint child not found! Please create an empty child named AttackPoint.");
        }
    }
    
    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (_timer <= 0)
        {
            _animator.SetTrigger("IsAttacking");
           
            _timer = _runTimeCombatData.attackCooldown;
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _runTimeCombatData.weaponRange, _runTimeCombatData.enemyLayer);
        if (enemies != null && enemies.Length > 0)
        {
            //enemies[0].GetComponent<EnemyHealth>().TakeDamage(-damage);
            
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.color =  Color.blue;
        Gizmos.DrawWireSphere(_attackPoint.position, _runTimeCombatData.weaponRange);
    }
}
