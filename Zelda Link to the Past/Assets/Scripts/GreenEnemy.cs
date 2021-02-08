using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : Enemy
{

    public Transform playerPos;
    public float chaseRadius;
    public float attackRadius;
    //public Transform startPos;

    private Rigidbody2D rb;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        CheckDistance();
    }
    
    //Check the distance of the player to the enemy to trigger the MoveTowards
    void CheckDistance(){

        if(Vector3.Distance(playerPos.position, transform.position) <= chaseRadius && Vector3.Distance(playerPos.position, transform.position) > attackRadius){
            rb.MovePosition(Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.fixedDeltaTime));
        }
    }

    //Draws the radius
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
