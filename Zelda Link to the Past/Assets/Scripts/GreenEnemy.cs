using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : Enemy
{

    public Transform playerPos;
    public float chaseRadius;
    public float attackRadius;
    public Transform startPos;
    public Animator animator;

    [HideInInspector] public Rigidbody2D rb;

    void Start()
    {
        enemyState = EnemyState.IDLE;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("inRange", true);
    }

    void FixedUpdate()
    {
        CheckDistance();
    }
    
    //Check the distance of the player to the enemy to trigger the MoveTowards
    public virtual void CheckDistance(){ //can override with virtual

        if(Vector3.Distance(playerPos.position, transform.position) <= chaseRadius && Vector3.Distance(playerPos.position, transform.position) > attackRadius){

            if(enemyState == EnemyState.IDLE || enemyState == EnemyState.MOVE && enemyState != EnemyState.STAGGER){
                Vector3 moveTowards = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.fixedDeltaTime);

                ChangeAnimation(moveTowards - transform.position);
                rb.MovePosition(moveTowards);
                ChangeState(EnemyState.MOVE);
                animator.SetBool("inRange", true);
            }
        }
        else if(Vector3.Distance(playerPos.position, transform.position) > chaseRadius)
        {
            animator.SetBool("inRange", false);
        }
    }

    //Change animation based on direction
    public void ChangeAnimation(Vector2 direction){
        direction = direction.normalized;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }

    void ChangeState(EnemyState state){
        if(enemyState != state){
            enemyState = state;
        }
    }

    //Draws the radius
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
