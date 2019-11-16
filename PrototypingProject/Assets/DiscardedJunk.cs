using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardedJunk : MonoBehaviour
{
    Rigidbody myRigidBody;
    public float launchVelocity = 5f;
    [Range(0,5)]
    public float torqueForce = 1f;
    public float lifetime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myRigidBody.velocity = new Vector3(
            0.0f,
            launchVelocity,
            0.0f
            );
        myRigidBody.AddTorque(
                new Vector3
                (
                Random.Range(-torqueForce, torqueForce),
                Random.Range(-torqueForce, torqueForce),
                Random.Range(-torqueForce, torqueForce)
                )
            );
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lifetime);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject); // possibly create a list on the object pool for this
        }
    }
}
