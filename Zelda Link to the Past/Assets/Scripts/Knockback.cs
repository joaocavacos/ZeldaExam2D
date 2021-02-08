using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public float thrustForce;
    public float knockbackTime;

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Enemy")){
            Rigidbody2D enemyrb = other.GetComponent<Rigidbody2D>();

            if(enemyrb != null){
                StartCoroutine(KnockbackCoroutine(enemyrb));
            }
        }
    }

    private IEnumerator KnockbackCoroutine(Rigidbody2D enemyrb){

        if(enemyrb != null){
            Vector2 forceDirection = enemyrb.transform.position - transform.position; //The force direction
            Vector2 force = forceDirection.normalized * thrustForce;

            enemyrb.velocity = force;
            yield return new WaitForSeconds(knockbackTime);
            enemyrb.velocity = Vector2.zero;
        }
    }

}
