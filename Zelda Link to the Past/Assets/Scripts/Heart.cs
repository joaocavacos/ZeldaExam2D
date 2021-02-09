using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectibles
{

    public FloatValue playerHealth;
    public FloatValue heartsContainer;
    public float healAmmount;

    [Header ("Sounds")]
    public AudioSource healSound;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger){
            playerHealth.runtimeValue += healAmmount;
            //healSound.Play();

            if(playerHealth.runtimeValue > heartsContainer.runtimeValue * 2f){ //Set to max hp if it's more
                playerHealth.runtimeValue = heartsContainer.runtimeValue * 2f;
            }
            collectibleSignal.Raise(); //Send signal, to check hearts to increase
            Destroy(this.gameObject);
        }
    }
}
