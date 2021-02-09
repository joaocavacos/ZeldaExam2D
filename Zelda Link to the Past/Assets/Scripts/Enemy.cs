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
    [Header ("State Machine")]
    public EnemyState enemyState;

    [Header ("Enemy Stats")]
    public FloatValue maxHealth;
    public float health;
    public float damage;
    public float speed;

    [Header ("Death")]
    public GameObject deathEffect;
    public LootTable thisLoot;

    [Header ("Sounds")]
    public AudioSource enemyDead;




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
            enemyDead.Play();
            DeathEffect();
            MakeLoot();
            Destroy(this.gameObject);
        }
    }

    private void MakeLoot(){
        if(thisLoot != null){
            Collectibles current = thisLoot.LootCollectible();

            if(current != null){
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    private void DeathEffect()
    {
        if(deathEffect != null){
            GameObject effect = Instantiate(deathEffect,transform.position, Quaternion.identity);
            Destroy(effect, 1f);
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
