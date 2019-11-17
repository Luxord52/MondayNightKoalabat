using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 i_Turning;
    float i_Throttle;
    public float moveSpeed = 5;

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
