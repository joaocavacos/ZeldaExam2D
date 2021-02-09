using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gems : Collectibles
{
    public int gems;

    [Header ("Sounds")]
    public AudioSource gemPick;

    void Start() {
        collectibleSignal.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger){
            //gemPick.Play();
            gems += 1;
            collectibleSignal.Raise();
            Destroy(this.gameObject);

        }
    }
}
