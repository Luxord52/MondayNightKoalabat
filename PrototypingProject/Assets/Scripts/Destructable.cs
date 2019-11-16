using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

    public int health;
	// Use this for initialization
	void Start () {
        health = 10;
	}
	
	// Update is called once per frame
	void Update () {
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    public void damaged(int damage)
    {
        health -= damage;
        
    }
}
