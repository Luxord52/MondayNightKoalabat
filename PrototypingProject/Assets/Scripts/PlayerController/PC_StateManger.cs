using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_StateManger : MonoBehaviour
{
    // team variable Integer
    public int team;
    public int joyNum;

    public enum MoveState
    {
        None,
        Idle,
        Run,
        Sliding,
        Jumping,
        Falling,
        //add a using weapon wheel state 
    }
    new public MoveState myMoveState = MoveState.None;

    public enum StanceState
    {
        None,
        Standing,
        Crouching,
    }
    new public StanceState myStanceState = StanceState.None;

    public PC_InputState myInput;
    public PC_InputManager myInputManager;
    public PC_Movement myMovement;


    public Camera myCamera;

    Vector3 DesiredDirection;
    void Update()
    {
        // get the input from controller.
        myInput = myInputManager.GetCachesInputState();

        // call movement code 
        myMovement.MoveMe(myInput);
        // call look code
        myMovement.JoyLook(myInput, myCamera);
        myMovement.BodyRotation(myInput,myCamera);
        // MovementState change
        // Stance State change
        // Action State change? 
    }


}
