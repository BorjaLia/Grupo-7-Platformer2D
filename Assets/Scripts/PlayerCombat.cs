using System;
using UnityEngine;
public class PlayerCombat : MonoBehaviour
{
     private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("IsAttacking");
        }
    }

}
