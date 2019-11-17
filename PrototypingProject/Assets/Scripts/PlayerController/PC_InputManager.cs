using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC_InputManager : MonoBehaviour
{
    PC_InputState CurrentInputState;
    PC_InputState CachedInputState;
    public Text TestingText;
    public PC_StateManger myStateManager;
    void Start()
    {
        CurrentInputState = new PC_InputState();
        CachedInputState = new PC_InputState();
    }
    void Update()
    {
        GetInput(myStateManager.joyNum);
    }
    void GetInput(int joyNum)
    {
        //FaceButtons
        CurrentInputState.Set_A(Input.GetButton("joy"+ joyNum+"aButton"));
        CurrentInputState.Set_B(Input.GetButton("joy" + joyNum + "bButton"));
        CurrentInputState.Set_X(Input.GetButton("joy" + joyNum + "xButton"));
        CurrentInputState.Set_Y(Input.GetButton("joy" + joyNum + "yButton"));
        //facebuttons down
        CurrentInputState.Set_Adn(Input.GetButtonDown("joy" + joyNum + "aButton"));
        CurrentInputState.Set_Bdn(Input.GetButtonDown("joy" + joyNum + "bButton"));
        CurrentInputState.Set_Xdn(Input.GetButtonDown("joy" + joyNum + "xButton"));
        CurrentInputState.Set_Ydn(Input.GetButtonDown("joy" + joyNum + "yButton"));
        //D-Pad Input
        CurrentInputState.Set_D_PadVertical(Input.GetAxis("joy" + joyNum + "dVertical"));
        CurrentInputState.Set_D_PadHorizontal(Input.GetAxis("joy" + joyNum + "dHorizontal"));
        //Shoulder Buttons
        CurrentInputState.Set_LeftBumper(Input.GetButton("joy" + joyNum + "LeftBumper"));
        CurrentInputState.Set_RightBumper(Input.GetButton("joy" + joyNum + "RightBumper"));
        CurrentInputState.Set_LeftTrigger(Input.GetAxis("joy" + joyNum + "LeftTrigger"));
        CurrentInputState.Set_RightTrigger(Input.GetAxis("joy" + joyNum + "RightTrigger"));
        //Shoulder Buttons Down
        CurrentInputState.Set_LeftBumperDn(Input.GetButtonDown("joy" + joyNum + "LeftBumper"));
        CurrentInputState.Set_RightBumperDn(Input.GetButtonDown("joy" + joyNum + "RightBumper"));
        //Joystick
        CurrentInputState.Set_LeftJoystickHorizontal(Input.GetAxis("joy" + joyNum + "LeftHorizontal"));
        CurrentInputState.Set_LeftJoystickVertical(Input.GetAxis("joy" + joyNum + "LeftVertical"));
        CurrentInputState.Set_RightJoystickHorizontal(Input.GetAxis("joy" + joyNum + "RightHorizontal"));
        CurrentInputState.Set_RightJoystickVertical(Input.GetAxis("joy" + joyNum + "RightVertical"));
       // CurrentInputState.Set_LeftJoystickClick(Input.GetButton("joy" + joyNum + "LeftClick"));
       // CurrentInputState.Set_RightJoystickClick(Input.GetButton("joy" + joyNum + "RightClick"));
        //Start and back
        CurrentInputState.Set_Start(Input.GetButtonDown("joy" + joyNum + "Start"));
        CurrentInputState.Set_Back(Input.GetButtonDown("joy" + joyNum + "Back"));
        TestingText.text = GetDebugText();
        CachedInputState = CurrentInputState.GetState();
    }
    
    string GetDebugText()
    {
        string debugText = "";
        // Face Buttons
        debugText += "A button:" + CurrentInputState.Get_A() + "\n";
        debugText += "B button:" + CurrentInputState.Get_B() + "\n";
        debugText += "X button:" + CurrentInputState.Get_X() + "\n";
        debugText += "Y button:" + CurrentInputState.Get_Y() + "\n";
        //D-Pad Input
        debugText += "D-Pad Horizontal Input:" + CurrentInputState.Get_D_PadHorizontal().ToString() + "\n";
        debugText += "D-Pad Vertical Input:" + CurrentInputState.Get_D_PadVertical().ToString() + "\n";
        //Shoulder Buttons
        debugText += "Left Bumper:" + CurrentInputState.Get_LeftBumper() + "\n";
        debugText += "Right Bumper:" + CurrentInputState.Get_RightBumper() + "\n";

        debugText += "Left Trigger:" + CurrentInputState.Get_LeftTrigger().ToString() + "\n";
        debugText += "Right Trigger:" + CurrentInputState.Get_RightTrigger().ToString() + "\n";
        //Joystick 
        debugText += "Left Joy H:" + CurrentInputState.Get_LeftJoystickHorizontal().ToString() + "\n";
        debugText += "Left Joy V:" + CurrentInputState.Get_LeftJoystickVertical().ToString() + "\n";
        debugText += "Right Joy H:" + CurrentInputState.Get_RightJoystickHorizontal().ToString() + "\n";
        debugText += "Right Joy V:" + CurrentInputState.Get_RightJoystickVertical().ToString() + "\n";
        debugText += "Left Joy Click:" + CurrentInputState.Get_LeftJoystickClick() + "\n";
        debugText += "Right Joy Click:" + CurrentInputState.Get_RightJoystickClick() + "\n";
        //Start Back Buttons
        debugText += "Start Button:" + CurrentInputState.Get_Start() + "\n";
        debugText += "Back Button:" + CurrentInputState.Get_Back() + "\n";

        return debugText;
    }
    public PC_InputState GetCachesInputState()
    {
        return CachedInputState;
    }
    
}
