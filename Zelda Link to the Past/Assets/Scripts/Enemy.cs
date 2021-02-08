using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;
    public int damage;
    public float speed;

    private HeartSystem heartSystem;

    void Start()
    {
        heartSystem = new HeartSystem(health);
    }

    void Update()
    {
        
    }
}
