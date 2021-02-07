using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkAttack : MonoBehaviour
{
    private LinkMovement linkMovement;

    void Start()
    {
        linkMovement = GetComponent<LinkMovement>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && linkMovement.playerState != PlayerState.ATTACK){
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine(){

        linkMovement.animator.SetBool("Attacking", true);
        linkMovement.playerState = PlayerState.ATTACK;
        yield return null;
        linkMovement.animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(1f);
        linkMovement.playerState = PlayerState.IDLE;
    }
}
