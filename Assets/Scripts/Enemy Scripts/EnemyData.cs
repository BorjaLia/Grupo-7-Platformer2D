using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy  movement parameters")]
    public float speed = 8.0f;

    [Header("Enemy Combat parameters")]
    public int maxHealth = 5;
    public int damage = 1;
    public float coolDown = 0.3f;
    
    [Header("Enemy Combat Range parameters")]
    public float forgetRange = 0.6f;
    public float seekRange = 0.4f;
    public float attackRange = 0.1f;
    public float damageDistance = 0.1f;
}
