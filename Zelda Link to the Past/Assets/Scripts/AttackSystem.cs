using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{

    public float thrustForce;
    public float knockbackTime;
    public float damage;
    private void Start() {
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Enemy")){
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if(rb != null){
                if(other.gameObject.CompareTag("Enemy") && other.isTrigger){
                    rb.GetComponent<Enemy>().enemyState = EnemyState.STAGGER;
                    other.GetComponent<Enemy>().Attack(rb, knockbackTime, damage);
                }

                Vector2 forceDirection = rb.transform.position - transform.position;
                Vector2 force = forceDirection.normalized * thrustForce;

                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }



}
