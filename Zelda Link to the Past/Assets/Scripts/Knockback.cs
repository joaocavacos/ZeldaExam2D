using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public float thrustForce;
    public float knockbackTime;

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Enemy")){
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if(rb != null){
                if(other.CompareTag("Enemy")){
                    rb.GetComponent<Enemy>().enemyState = EnemyState.STAGGER;
                    StartCoroutine(KnockbackCoroutine(rb,knockbackTime,thrustForce));
                }
            }
        }
    }

    private IEnumerator KnockbackCoroutine(Rigidbody2D rb, float knockbackTime, float thrustForce){

        if(rb != null){
            Vector2 forceDirection = rb.transform.position - transform.position; //The force direction
            Vector2 force = forceDirection.normalized * thrustForce; //Normalize the vector and add force

            rb.velocity = force;
            yield return new WaitForSeconds(knockbackTime);
            rb.velocity = Vector2.zero;
            rb.GetComponent<Enemy>().enemyState = EnemyState.IDLE;
            rb.velocity = Vector2.zero;
        }
    }
}
