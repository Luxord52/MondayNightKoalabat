using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    Rigidbody myRigidbody;
    Vector3 direction;
    public float force;
    public float maxRange;
    float deltaPosition;

    void Start()
    {
        direction = gameObject.transform.forward;
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.AddForce(direction*force);
    }

    // Update is called once per frame
    void Update()
    {
        deltaPosition += myRigidbody.velocity.magnitude * Time.deltaTime;
        if (deltaPosition >= maxRange)
        {
            Destroy(gameObject);
        }
       // bullets which behave oddly in flight? 
    }
}
