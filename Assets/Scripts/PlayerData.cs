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
    public float _hurtTimer = 0.2f;
}
