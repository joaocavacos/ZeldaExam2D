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
    [Header ("Player Stats")]
    public float speed;

    private Rigidbody2D rb;

    private Animator animator;

    Vector2 movement;

    [Header ("State Machine")]
    public PlayerState playerState;
    [Header ("Scriptable Objects")]
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;

    public GameObject arrow;

    [Header ("Sounds")]
    public AudioSource enemyHit;
    public AudioSource arrowShot;

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
        else if(Input.GetKeyDown(KeyCode.R) && playerState != PlayerState.ATTACK && playerState != PlayerState.STAGGER)
        {
            StartCoroutine(ArrowCoroutine());
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
        enemyHit.Play();
        yield return new WaitForEndOfFrame();
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.5f);
        playerState = PlayerState.MOVE;
    }
    private IEnumerator ArrowCoroutine(){

        playerState = PlayerState.ATTACK;
        arrowShot.Play();
        yield return new WaitForEndOfFrame();
        MakeArrow();
        yield return new WaitForSeconds(0.5f);
        playerState = PlayerState.MOVE;
    }

    private void MakeArrow(){

        Vector2 direction = new Vector2(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical"));
        Arrow arrowGO = Instantiate(arrow, transform.position, Quaternion.identity).GetComponent<Arrow>();

        arrowGO.SetupArrow(direction, ArrowRotation());
    }

    Vector3 ArrowRotation(){
        float direction = Mathf.Atan2(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical")) * Mathf.Rad2Deg;

        return new Vector3(0,0,direction);
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
