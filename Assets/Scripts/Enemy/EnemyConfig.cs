using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int damage;
    public float moveSpeed;
    public float chaseSpeed;
    public float detectionRange;
    public float attackRange;
}
