using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_StateMaanger : MonoBehaviour
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

    Vector3 DesiredDirection;



    void Update()
    {
        // get the input from controller.
        DesiredDirection = new Vector3(
                   Input.GetAxis("joy" + joyNum + "Horizontal"),
                   0.0f,
                   Input.GetAxis("joy" + joyNum + "Vertical"));
        DesiredDirection.Normalize();



        // call movement code
        // call look code
        
        // MovementState change
        // Stance State change
        // Action State change? 
    }


}
