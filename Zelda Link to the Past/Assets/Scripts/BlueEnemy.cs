using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : GreenEnemy
{
    [Header ("Patrol Points and Path")]
    public Transform[] path;
    public Transform nextPoint;
    public int currentPoint;
    public float roundedDistance;

    public override void CheckDistance(){

        //Check if is in range of player
        if(Vector3.Distance(playerPos.position, transform.position) <= chaseRadius && Vector3.Distance(playerPos.position, transform.position) > attackRadius){

            if(enemyState == EnemyState.IDLE || enemyState == EnemyState.MOVE && enemyState != EnemyState.STAGGER){
                Vector3 moveTowards = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.fixedDeltaTime);

                ChangeAnimation(moveTowards - transform.position);
                rb.MovePosition(moveTowards);
                animator.SetBool("inRange", true);
            }
        }
        else if(Vector3.Distance(playerPos.position, transform.position) > chaseRadius) // if not, continues patrol
        {
            if(Vector3.Distance(transform.position, path[currentPoint].position) > roundedDistance){ //Check which point is closer, move there
                Vector3 moveTowards = Vector3.MoveTowards(transform.position, path[currentPoint].position, speed * Time.fixedDeltaTime);

                ChangeAnimation(moveTowards - transform.position);
                rb.MovePosition(moveTowards);
            }
            else //When reached goal change to other point
            {
                ChangePoint();
            }
            
        }
    }

    private void ChangePoint(){ //Check the point to follow

        if(currentPoint == path.Length - 1){
            currentPoint = 0;
            nextPoint = path[0];
        }
        else
        {
            currentPoint++;
            nextPoint = path[currentPoint];
        }

    }
}
