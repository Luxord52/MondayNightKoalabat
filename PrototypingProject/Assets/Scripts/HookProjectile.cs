using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookProjectile : MonoBehaviour
{
    Rigidbody myRigidBody;
    Vector3 direction;
    public float force;
    public float maxRange;
    float deltaPosition;
    public bool locked;

    public void FireAgain()
    {
        locked = false;
        myRigidBody.constraints = RigidbodyConstraints.None;
        deltaPosition = 0.0f;
        direction = gameObject.transform.forward;
        myRigidBody.AddForce(direction * force);
        gameObject.GetComponent<Collider>().enabled = true;
    }
    // Start is called before the first frame update
    void Awake()
    {
        direction = gameObject.transform.forward;
        myRigidBody = GetComponent<Rigidbody>();
        myRigidBody.AddForce(direction * force);
    }

    // Update is called once per frame
    void Update()
    {
        deltaPosition += myRigidBody.velocity.magnitude * Time.deltaTime;
        if (deltaPosition >= maxRange)
        {
            gameObject.SetActive(false);
        }
    }
    void OnCollisionEnter(Collision collider)
    {
        locked = true;
        myRigidBody.constraints = RigidbodyConstraints.FreezePosition;
        gameObject.GetComponent<Collider>().enabled = false;

    }
}
