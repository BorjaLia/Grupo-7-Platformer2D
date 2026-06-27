using System;
using System.Linq;
using UnityEngine;
public class PlayerCombat : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField] private PlayerData combatData; 
     private Animator _animator;
     private PlayerData _runTimeCombatData;
     private float _timer;
     

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (combatData != null)
            _runTimeCombatData = Instantiate(combatData);
        else
            Debug.LogError("Player Combat Data is missing!");
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
}
