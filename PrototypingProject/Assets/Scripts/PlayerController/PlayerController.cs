using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 i_Turning;
    float i_Throttle;
    float f_Speed;
    public float MaxSpeed = 5;
    public float turnSpeed = 2;
    public float dragForce = 0.9f;
    public Rigidbody myBody;

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
            if (i_Throttle > 0.04)
            {
                if (f_Speed < MaxSpeed)
                {
                    f_Speed += i_Throttle * 5;
                }
                else
                {
                    f_Speed = MaxSpeed;
                }
            }
            else
            {
                
                f_Speed = f_Speed*(1 - dragForce);
                Debug.Log(f_Speed);
            }
            myBody.velocity = myBody.transform.TransformDirection(Vector3.forward) * f_Speed;
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
