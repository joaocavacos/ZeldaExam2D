using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    IDLE,
    MOVE,
    ATTACK,
    STAGGER
}
public class Enemy : MonoBehaviour
{
    public EnemyState enemyState;
    public int health;
    public int damage;
    public float speed;

    private HeartSystem heartSystem;

    void Start()
    {
        heartSystem = new HeartSystem(health);
    }

    
}
