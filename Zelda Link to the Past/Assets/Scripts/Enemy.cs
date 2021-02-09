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
    public FloatValue maxHealth;
    public float health;
    public float damage;
    public float speed;


    void Awake() {
        health = maxHealth.initialValue;
    }

    public void Attack(Rigidbody2D rigidbody2D, float knockbackTime, float damage){ //Knockback and damage the enemy

        StartCoroutine(KnockbackCoroutine(rigidbody2D, knockbackTime));
        TakeDamage(damage);
    }

    private void TakeDamage(float damage){ //Damage method

        health -= damage;

        if(health <= 0){
            Destroy(this.gameObject);
        }
    }
    

    private IEnumerator KnockbackCoroutine(Rigidbody2D rb, float knockbackTime){

        if(rb != null){

            yield return new WaitForSeconds(knockbackTime);
            rb.velocity = Vector2.zero;
            enemyState = EnemyState.IDLE;
            rb.velocity = Vector2.zero;
        }
    }

    
}
