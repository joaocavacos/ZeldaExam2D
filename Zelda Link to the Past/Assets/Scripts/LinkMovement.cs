using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    IDLE,
    ATTACK,
    MOVE,
    STAGGER
}

public class LinkMovement : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;

    public PlayerState playerState;
    private LinkMovement linkMovement;


    void Start() {
        playerState = PlayerState.IDLE;
    }
    
    void Update()
    {
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");  
    }

	void FixedUpdate()
	{
        if(playerState != PlayerState.ATTACK){
            if(movement != Vector2.zero){
                Move();

                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetBool("Moving", true);
        
                playerState = PlayerState.MOVE;
            }
            else
            {
                animator.SetBool("Moving", false);
            
                playerState = PlayerState.IDLE;
            }
        }
        
	}

    private void Move(){
        
            if (Input.GetKey(KeyCode.LeftShift)) //Sprint
		    {
                rb.MovePosition(rb.position + movement.normalized * sprintSpeed * Time.fixedDeltaTime);
		    }
		    else
		    {
                rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime); //Normal movement, normalized so the diagonal stays the same speed
            }
    }

}
