using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Movement : MonoBehaviour
{
    public float maxSpeed;
    public float myAcceleration;
    public Rigidbody myRigidBody;


    //mouselook variables 
    Vector3 rotation;
    public float lookSpeed;
    public float AimSpeed;

    //HeadBobing
    private float timer = 0.0f;
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount;
    float midpoint = 0.5f;

    public void MoveMe(PC_InputState myInput)
    {
        Vector3 DesiredDirection;
        DesiredDirection = new Vector3(
            myInput.Get_LeftJoystickHorizontal(),
            0.0f,
            myInput.Get_LeftJoystickVertical()
            );
        if (DesiredDirection.magnitude > .5)
        {
            if (myRigidBody.velocity.magnitude < maxSpeed)
            {
                myRigidBody.velocity = myRigidBody.velocity + DesiredDirection.normalized * myAcceleration;
            }
            else
            {
                myRigidBody.velocity = DesiredDirection.normalized * maxSpeed;
            }
        }
    }
    public void HeadBob(Rigidbody myRigidBody, Camera myCamera)
    {


        // take the camera position and shift it up and down in relation to the velocity of the player 

        //stolen code here 
        float waveslice = 0.0f;
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");

        Vector3 HeadHeight = myCamera.transform.localPosition;

        if (myRigidBody.velocity.x == 0f && myRigidBody.velocity.z == 0f)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer = timer + bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }
        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            HeadHeight.y = midpoint + translateChange;
        }
        else
        {
            HeadHeight.y = midpoint;
        }

        myCamera.transform.localPosition = HeadHeight;
    }

    public void JoyLook(PC_InputState myInput,Camera myCamera)
    {

        Vector3 currRotation;
        Vector3 DesRotation;

        
        rotation.y += myInput.Get_RightJoystickHorizontal() * 100;
        rotation.x += myInput.Get_RightJoystickVertical() * 100;
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        myCamera.transform.eulerAngles = (Vector2)rotation;// * lookSpeed;

        Debug.Log(rotation.ToString());
        // transform.eulerAngles = new Vector2(0,rotation.y)* lookSpeed;   
        // myCamera.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
        
    }

}
