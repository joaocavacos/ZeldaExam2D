using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaf : MonoBehaviour
{
    public Sprite destroyedSprite;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    public LootTable thisLoot;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    //Replaces the sprite to destroyed and disables collider
    public void DestroyLeaf(){
        spriteRenderer.sprite = destroyedSprite;
        boxCollider2D.enabled = false;
        MakeLoot();
    }

    public void MakeLoot(){
        if(thisLoot != null){
            Collectibles current = thisLoot.LootCollectible();

            if(current != null){
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}

