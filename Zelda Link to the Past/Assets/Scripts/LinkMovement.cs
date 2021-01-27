using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkMovement : MonoBehaviour
{
    public float speed;

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
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
            rb.MovePosition(rb.position + movement * (speed * 1.5f) * Time.fixedDeltaTime);
		}
		else
		{
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
        


	}
}
