using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkMovement : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude); //Speed is the magnitude, squared for better optimization
    }

	void FixedUpdate()
	{
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
