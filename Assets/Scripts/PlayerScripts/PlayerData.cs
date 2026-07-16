using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Player Movement Settings")]
    public float playerSpeed = 4.0f;
    public float playerJumpForce = 15.0f;
    public float playerDodgeForce = 15.0f;
    public float playerHurtForce = 3.0f;
    public float playerAirControlMultiplier = 0.5f;
    public LayerMask groundLayerMask;
    
    [Header("Player Health Settings")]
    public float maxHealth = 5f;

    [Header("Player Combat Settings")] 
    public float weaponRange = 0.3f;
    public float damage = 1.0f;
    public LayerMask enemyLayer;

    [Header("Player Cooldown Settings")]
    public float hurtTimer = 0.2f;
    public float dodgeTimer = 0.2f;
    public float attackCooldown = 0.5f;
    
    [Header("Player GroundCheck Settings")]
    public float groundCheckRadius = 0.1f;
}
