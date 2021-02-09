using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;

    public void SetupArrow(Vector2 velocity, Vector3 direction){

        rb.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.CompareTag("Enemy")){
            Destroy(this.gameObject);
        }
        
    }
}
