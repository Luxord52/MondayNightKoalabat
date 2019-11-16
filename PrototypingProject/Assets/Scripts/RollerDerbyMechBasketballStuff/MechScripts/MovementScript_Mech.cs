using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementScript_Mech : MonoBehaviour
{
    // Copy this and edit for mech/ tank controls? what does a racer control like? 
    /*
     * Keyboard Controls
     * 
     * Mouse Look
     * W - Throttle up, moves in the direction of mouse look
     * A - Strafe Left
     * S - Strafe Right
     * D - Throttle Down
     * 
     * Game Pad Controls 
     * Right Stick Look
     * Left Stick Strafe Left & Right, Throttle Foreward & Back
     * 
     */


    //Rigidbody myRigidBody;
    //Camera myCamera;
    public GameObject myGun;
    public GameObject myBody;
    float distanceToGround;

    //movement variables 
    // Vector3 DesiredDirection;
    [Header("StanceHeights")]
    public float playerStanding;        // player standing height max
    public float playerCrouch;          // player crouching height min
    [Header("Movement Variables")]
    public float ManualForce;
    public float AutomaticForce;
    public float CrouchForce;
    float runForce;              // force CURRENTLYapplied to the player when running

    public float maxRunSpeed;           // maximum speed when running
    [Range(0,1)]
    public float runDragStrength;       // the drag impacting the player when moving 

    public float turnSpeed;             // limits how quickly the body turns to catch up with the camera
    public float jumpForce;             // affects jump height and impulse 
    public float jumpDecayRate;         // affects the duration of the jump impulse

    float currRunSpeed;                 // how quickly player is currently moving

    Vector3 slideDirection;             // the direction the player is sliding in
    public float slideForce;            // the initial slide impulse
    public float slideDecayRate;        // how quickly the slide force decays

    //mouselook variables 
    Vector2 rotation = new Vector2(0, 0);
    public float lookSpeed;
    public float AimSpeed;

    //Stolen code variables 
    private float timer = 0.0f;
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount;
    float midpoint = 0.5f;


    public void MoveMe(Rigidbody myRigidBody, Vector3 DesiredDirection)
    {
        //limiting input to forewards backwards when moving the player
        Vector3 throttleControl = new Vector3(0.0f, 0.0f, DesiredDirection.z);
        currRunSpeed = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(myRigidBody.velocity.x, 2) + Mathf.Pow(myRigidBody.velocity.y, 2)));
        //if not moving faster than max speed
        if (currRunSpeed < maxRunSpeed)
        {
           // Debug.Log("c");
            //apply force
            //myRigidBody.AddForce(DesiredDirection * runForce);
            myRigidBody.AddRelativeForce(myBody.transform.TransformDirection(throttleControl) * runForce);
        }
        
        float clampedVelocityX = myRigidBody.velocity.x * (1.0f - runDragStrength);
        clampedVelocityX = Mathf.Clamp(clampedVelocityX, -maxRunSpeed, maxRunSpeed);

        float clampedVelocityZ = myRigidBody.velocity.z * (1.0f - runDragStrength);
        clampedVelocityZ = Mathf.Clamp(clampedVelocityZ, -maxRunSpeed, maxRunSpeed);

        myRigidBody.velocity = new Vector3(
            clampedVelocityX,
            myRigidBody.velocity.y,
            clampedVelocityZ
        
        );

    }
    public void BodyRotation(Camera myCamera, string playerStateString, Vector3 DesiredDireciton)
    {
        if (playerStateString == "Automatic")
        {
            float targetBodyRotY = myCamera.transform.eulerAngles.y;
            float currentBodyRotY = myBody.transform.eulerAngles.y;

            float deltaAngle = Mathf.DeltaAngle(currentBodyRotY, targetBodyRotY);

            currentBodyRotY += (deltaAngle) * turnSpeed * Time.deltaTime;
            myBody.transform.localRotation = Quaternion.Euler(
                0.0f,
                currentBodyRotY,
                0.0f
                );
        }
        else
        {
            // rotates left if desired direction positive rotates right if desired direction negative.
            float targetBodyRotY = myBody.transform.eulerAngles.y + DesiredDireciton.x * 45;
            float currentBodyRotY = myBody.transform.eulerAngles.y;

            float deltaAngle = Mathf.DeltaAngle(currentBodyRotY, targetBodyRotY);

            currentBodyRotY += (deltaAngle) * turnSpeed * Time.deltaTime;
            myBody.transform.localRotation = Quaternion.Euler(
                0.0f,
                currentBodyRotY,
                0.0f
                );
        }
    }

    // camera position and motion Scripts 
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
    public void MouseLook(Camera myCamera)
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        myCamera.transform.eulerAngles = (Vector2)rotation * lookSpeed;
        // transform.eulerAngles = new Vector2(0,rotation.y)* lookSpeed;   
        // myCamera.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
    }

    // Gun Position and Motion Scripts
    public void AimWeapon(Camera myCamera)
    {
        RaycastHit hit;
        if (Physics.Raycast(myCamera.transform.position + myCamera.transform.forward, myCamera.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(myCamera.transform.position, myCamera.transform.forward * hit.distance, Color.yellow);
            Debug.DrawRay(myGun.transform.position, myGun.transform.forward * hit.distance, Color.red);
            Vector3 _direction = (hit.point - myGun.transform.position);
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);
            myGun.transform.rotation = Quaternion.Slerp(myGun.transform.rotation, _lookRotation, Time.deltaTime * AimSpeed);
        }
        else
        {
            Debug.DrawRay(myCamera.transform.position, myCamera.transform.forward * 1000, Color.white);
            Debug.DrawRay(myGun.transform.position, myGun.transform.forward * 1000, Color.red);
            Vector3 _direction = (myCamera.transform.forward * 1000 - myGun.transform.position);
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);
            myGun.transform.rotation = Quaternion.Slerp(myGun.transform.rotation, _lookRotation, Time.deltaTime * AimSpeed);
        }
    }

    // mobility States 
    public void Jump(Rigidbody myRigidBody)
    {
        StartCoroutine(JumpEnum(myRigidBody));
    }
    IEnumerator JumpEnum(Rigidbody myRigidBody)
    {
        myRigidBody.velocity = new Vector3(
                           myRigidBody.velocity.x,
                           0.0f,
                           myRigidBody.velocity.z
                       );
        myRigidBody.AddForce(Vector3.up * jumpForce);
        yield return null;
        
        float currentForce = jumpForce/5;

        while (Input.GetButton("Jump") && currentForce > 0)
        {
            myRigidBody.AddForce(Vector3.up * currentForce);

            currentForce -= jumpDecayRate * Time.deltaTime;
            yield return null;
        }
    }
    public void Slide(Rigidbody myRigidBody, StateManager_Mech MyManager, Vector3 DesiredDirection)
    {
        StartCoroutine(SlideEnum(myRigidBody, MyManager,DesiredDirection));
    }
    public Vector3 GetSlideDirection()
    {
        return slideDirection;
    }
    IEnumerator SlideEnum(Rigidbody myRigidBody, StateManager_Mech MyManager, Vector3 DesiredDirection)
    {
        //Debug.Log("Ping");
        SetRunForce(50);
        slideDirection = new Vector3(myRigidBody.velocity.x, 0.0f, myRigidBody.velocity.z);
        myRigidBody.velocity = Vector3.zero;

        Debug.DrawRay(transform.position + Vector3.up, slideDirection * 100, Color.blue);
        myRigidBody.AddForce(slideDirection * slideForce);
        yield return null;

        float currentForce = slideForce / 5;

        while (MyManager.GetPlayerState().ToString() == "Sliding" && currentForce > 0)
        {
            // Debug.Log("Pong");
            myRigidBody.AddForce(slideDirection * currentForce);
            currentForce -= slideDecayRate * Time.deltaTime;
            yield return null;
        }
    }
    public void Sprint()
    {
        SetRunForce(AutomaticForce);
        bobbingSpeed = 0.25f;
    }

    //mobility Stances
    public void Crouch(StateManager_Mech MyManager)
    {
        StartCoroutine(CrouchEnum(MyManager));
    }
    IEnumerator CrouchEnum(StateManager_Mech MyManager)
    {
        CapsuleCollider body = GetComponent<CapsuleCollider>();

        if (MyManager.GetPlayerStance().ToString() == "Crouching" && MyManager.GetPlayerState().ToString() != "Sliding" && MyManager.IsOnGround())
        {
            SetRunForce(CrouchForce);
            bobbingSpeed = 0.13f;
        }
        yield return null;

        while (MyManager.GetPlayerStance().ToString()=="Crouching" && body.height > playerCrouch)
        {
            if ( runForce>50 &&MyManager.GetPlayerState().ToString() != "Sliding" && MyManager.IsOnGround())
            {
                SetRunForce(CrouchForce);
            }
            body.height -= turnSpeed * Time.deltaTime;
            yield return null;
        }
    }
    public void Stand(StateManager_Mech MyManager)
    {
        StartCoroutine(StandEnum(MyManager));
    }
    IEnumerator StandEnum(StateManager_Mech MyManager)
    {
        CapsuleCollider body = GetComponent<CapsuleCollider>();

        //SetRunForce(75);
        yield return null; 

        while (MyManager.GetPlayerStance().ToString() == "Standing" && body.height < playerStanding)
        {
            body.height += turnSpeed * Time.deltaTime;
            if (body.height > playerStanding)
            {
                if (MyManager.GetPlayerState().ToString() == "Sprinting")
                {
                    SetRunForce(AutomaticForce);
                    bobbingSpeed = 0.25f;
                }
                else
                {
                    SetRunForce(ManualForce);
                    bobbingSpeed = .18f;
                }
            }
            yield return null;
        }
    }



    
    public void SetRunForce(float force)
    {
        runForce = force;
    }
    public float GetCurrRunSpeed()
    {
        return currRunSpeed;
    }
}
