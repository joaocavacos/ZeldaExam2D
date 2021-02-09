using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    IDLE,
    ATTACK,
    MOVE,
    STAGGER
}

public class Link : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;

    private Animator animator;

    Vector2 movement;

    public PlayerState playerState;
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;

    void Start() {
        playerState = PlayerState.MOVE;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");  
    }

	void FixedUpdate()
	{
        if(Input.GetKeyDown(KeyCode.E) && playerState != PlayerState.ATTACK && playerState != PlayerState.STAGGER){
            StartCoroutine(AttackCoroutine());
        }
        else if(playerState == PlayerState.IDLE || playerState == PlayerState.MOVE)
        {
            UpdateMove();
        }
	}

    private void Move(){
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    private void UpdateMove(){
        if(movement != Vector2.zero){
            Move();

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    private IEnumerator AttackCoroutine(){

        animator.SetBool("Attacking", true);
        playerState = PlayerState.ATTACK;
        yield return new WaitForEndOfFrame();
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.5f);
        playerState = PlayerState.MOVE;
    }

    public void Attack(float knockbackTime, float damage){

        currentHealth.runtimeValue -= damage;
        playerHealthSignal.Raise();

        if(currentHealth.runtimeValue > 0){
            StartCoroutine(KnockbackCoroutine(knockbackTime));
        }
        else
        {
            //Game over
            Debug.Log("Dead");
        }
    }

    private IEnumerator KnockbackCoroutine(float knockbackTime){

        if(rb != null){

            yield return new WaitForSeconds(knockbackTime);
            rb.velocity = Vector2.zero;
            playerState = PlayerState.IDLE;
            rb.velocity = Vector2.zero;
        }
    }

}
