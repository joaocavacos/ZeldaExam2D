using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Breakable")){

            //For the leaf
            if(other.GetComponent<Leaf>() != null){
                other.GetComponent<Leaf>().DestroyLeaf();
            }
        }
    }
}
