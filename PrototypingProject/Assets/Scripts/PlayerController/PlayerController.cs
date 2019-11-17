using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    // player input variables 
    Vector2 i_Turning;
    float i_Throttle;
    float i_Brake;

    //Player variables 
    float f_Speed;
    float f_Acceleration = 50;
    //Vector3 v_Velocity;
    public float MaxSpeed = 5;
    public float turnSpeed = 2;
    public float dragForce = 0.9f;

    //Player referenced components
    public Rigidbody myRigidBody;
    public GameObject myBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();   
    }

    void Move()
    {
        //v_Velocity = myRigidBody.velocity;
        //f_Speed = v_Velocity.magnitude;
        //Vector3 movement = new Vector3(i_Movement.x, 0, i_Movement.y) * moveSpeed * Time.deltaTime;
        //transform.Translate(movement);

        // body rotation
        {
            float targetBodyRotY = myBody.transform.eulerAngles.y + i_Turning.x * 45;
            float currentBodyRotY = myBody.transform.eulerAngles.y;

            float deltaAngle = Mathf.DeltaAngle(currentBodyRotY, targetBodyRotY);

            currentBodyRotY += (deltaAngle) * turnSpeed * Time.deltaTime;
            myBody.transform.localRotation = Quaternion.Euler(
                0.0f,
                currentBodyRotY,
                0.0f
                );
        }
        // gotta step on the gas Gas GAS
        {
            f_Speed = myRigidBody.velocity.magnitude;
            if (i_Throttle > 0.04)
            {
                if (f_Speed < MaxSpeed)
                {
                    // add force to rigid body, in foreward direction
                    myRigidBody.AddForce(myBody.transform.TransformDirection(Vector3.forward) * f_Acceleration);
                }
            }
            /*
            else
            {
                
                f_Speed = f_Speed*(1 - dragForce);
                Debug.Log(f_Speed);
            }
            */
            float clampedVelocityX = myRigidBody.velocity.x * (1.0f - dragForce);
            clampedVelocityX = Mathf.Clamp(clampedVelocityX, -MaxSpeed, MaxSpeed);

            float clampedVelocityZ = myRigidBody.velocity.z * (1.0f - dragForce);
            clampedVelocityZ = Mathf.Clamp(clampedVelocityZ, -MaxSpeed, MaxSpeed);

            myRigidBody.velocity = new Vector3(
                clampedVelocityX,
                myRigidBody.velocity.y,
                clampedVelocityZ
                );

        }
    }

    private void OnTurning(InputValue value)
    {
        // recieves input from left joystick 
        i_Turning = value.Get<Vector2>();

        Debug.Log("MOVING");
    }
    private void OnThrottle(InputValue axis)
    {
        i_Throttle = axis.Get<float>();
        Debug.Log(i_Throttle);
    }

}
