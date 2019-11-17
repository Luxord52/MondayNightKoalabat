using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_InputState
{
    // all of these variables are private and should not be accessible from other sources. 
    //Face buttons
    bool _A;
    bool _B;
    bool _X;
    bool _Y;
    //FaceButtons Down
    bool _Adn;
    bool _Bdn;
    bool _Xdn;
    bool _Ydn;
    //D - pad
    float _D_PadHorizontal;
    float _D_PadVertical;
    //Shoulder Buttons
    bool _LeftBumper;
    bool _RightBumper;
    float _LeftTrigger;
    float _RightTrigger;
    //Shoulder Buttons
    bool _LeftBumperDn;
    bool _RightBumperDn;
    float _LeftTriggerDn;
    float _RightTriggerDn;
    //joysticks
    float _LeftJoystickHorizontal;
    float _LeftJoystickVertical;
    float _RightJoystickHorizontal;
    float _RightJoystickVertical;
    bool _LeftJoystickClick;
    bool _RightJoystickClick;
    // Start Select Buttons
    bool _Start;
    bool _Back;

    //return the whole kit and kaboodle
    public PC_InputState GetState()
    {
        return this;


    }

    public PC_InputState GetDeepState()
    {
        PC_InputState inputCopy = new PC_InputState();

        inputCopy._A = _A;
        inputCopy._B= _B;
        inputCopy._X= _X;
        inputCopy._Y= _Y;
        //FaceButtons Down
        inputCopy._Adn= _Adn;
        inputCopy._Bdn= _Bdn;
        inputCopy._Xdn= _Xdn;
        inputCopy._Ydn= _Ydn;
        //D - pad
        inputCopy._D_PadHorizontal= _D_PadHorizontal;
        inputCopy._D_PadVertical= _D_PadVertical;
        //Shoulder Buttons
        inputCopy._LeftBumper= _LeftBumper;
        inputCopy._RightBumper= _RightBumper;
        inputCopy._LeftTrigger= _LeftTrigger;
        inputCopy._RightTrigger= _RightTrigger;
        //Shoulder Buttons down
        inputCopy._LeftBumperDn = _LeftBumperDn;
        inputCopy._RightBumperDn = _RightBumperDn;
        inputCopy._LeftTriggerDn = _LeftTriggerDn;
        inputCopy._RightTriggerDn = _RightTriggerDn;

        //joysticks
        inputCopy._LeftJoystickHorizontal= _LeftJoystickHorizontal;
        inputCopy._LeftJoystickVertical= _LeftJoystickVertical;
        inputCopy._RightJoystickHorizontal= _RightJoystickHorizontal;
        inputCopy._RightJoystickVertical= _RightJoystickVertical;
        //inputCopy._LeftJoystickClick= _LeftJoystickClick;
       // inputCopy._RightJoystickClick= _LeftJoystickClick;
        // Start Select Buttons
        inputCopy._Start= _Start;
        inputCopy._Back= _Back;

        return inputCopy;
    }
    public override string ToString()
    {
        string compiledInput = "";

        compiledInput = "A:" + Get_A().ToString() + "\n" +
                        "B:" + Get_B().ToString() + "\n" +
                        "X:" + Get_X().ToString() + "\n" +
                        "Y:" + Get_Y().ToString() + "\n" +

                        "Adn:" + Get_Adn().ToString() + "\n" +
                        "Bdn:" + Get_Bdn().ToString() + "\n" +
                        "Xdn:" + Get_Xdn().ToString() + "\n" +
                        "Ydn:" + Get_Ydn().ToString() + "\n" +

                        "D_PadHorizontal:" + Get_D_PadHorizontal().ToString() + "\n" +
                        "D_PadVertical:" + Get_D_PadVertical().ToString() + "\n" +

                        "LeftBumper:" + Get_LeftBumper().ToString() + "\n" +
                        "RightBumper:" + Get_RightBumper().ToString() + "\n" +
                        "LeftTrigger:" + Get_LeftTrigger().ToString() + "\n" +
                        "RightTrigger:" + Get_RightTrigger().ToString() + "\n" +

                        "LeftJoystickHorizontal:" + Get_LeftJoystickHorizontal().ToString() + "\n" +
                        "LeftJoystickVertical:" + Get_LeftJoystickVertical().ToString() + "\n" +
                        "RightJoystickHorizontal:" + Get_RightJoystickHorizontal().ToString() + "\n" +
                        "RightJoystickVertical:" + Get_RightJoystickVertical().ToString() + "\n" +
                       // "LeftJoystickClick:" + Get_LeftJoystickClick().ToString() + "\n" +
                        //"LeftJoystickClick:" + Get_LeftJoystickClick().ToString() + "\n" +

                        "Start:" + Get_Start().ToString() + "\n" +
                        "Back:" + Get_Back().ToString() + "\n";
        return compiledInput;
    }

    //Set Face Buttons
    public void Set_A(bool var)
    {
        _A = var;
    }
    public void Set_B(bool var)
    {
        _B = var;
    }
    public void Set_X(bool var)
    {
        _X = var;
    }
    public void Set_Y(bool var)
    {
        _Y = var;
    }
    //Face Buttons down
    public void Set_Adn(bool var)
    {
        _Adn = var;
    }
    public void Set_Bdn(bool var)
    {
        _Bdn = var;
    }
    public void Set_Xdn(bool var)
    {
        _Xdn = var;
    }
    public void Set_Ydn(bool var)
    {
        _Ydn = var;
    }
    //Set D - Pad
    public void Set_D_PadHorizontal(float var)
    {
        _D_PadHorizontal = var;
    }
    public void Set_D_PadVertical(float var)
    {
        _D_PadVertical = var;
    }
    //Set Shoulder Buttons
    public void Set_LeftBumper(bool var)
    {
        _LeftBumper = var;

    }
    public void Set_RightBumper(bool var)
    {
        _RightBumper = var;

    }
    public void Set_LeftTrigger(float var)
    {
        _LeftTrigger = var;
    }
    public void Set_RightTrigger(float var)
    {
        _RightTrigger = var;
    }
    //Set Shoulder Buttons Dn
    public void Set_LeftBumperDn(bool var)
    {
        _LeftBumperDn = var;
    }
    public void Set_RightBumperDn(bool var)
    {
         _RightBumperDn = var;
    }
    //Set Joysticks
    public void Set_LeftJoystickHorizontal(float var)
    {
        _LeftJoystickHorizontal = var;
    }
    public void Set_LeftJoystickVertical(float var)
    {
        _LeftJoystickVertical = var;
    }
    public void Set_RightJoystickHorizontal(float var)
    {
        _RightJoystickHorizontal = var;
    }
    public void Set_RightJoystickVertical(float var)
    {
        _RightJoystickVertical = var;
    }
    public void Set_LeftJoystickClick(bool var)
    {
        _LeftJoystickClick = var;
    }
    public void Set_RightJoystickClick(bool var)
    {
        _RightJoystickClick = var;
    }
    //Set Start And back buttons
    public void Set_Start(bool var)
    {
        _Start = var;
    }
    public void Set_Back(bool var)
    {
        _Back = var;
    }


    //Get Face Buttons
    public bool Get_A()
    {
        return _A ;
    }
    public bool Get_B()
    {
        return _B;
    }
    public bool Get_X()
    {
        return _X;
    }
    public bool Get_Y()
    {
        return _Y;
    }
    //face Buttons down
    public bool Get_Adn()
    {
        return _Adn;
    }
    public bool Get_Bdn()
    {
        return _Bdn;
    }
    public bool Get_Xdn()
    {
        return _Xdn;
    }
    public bool Get_Ydn()
    {
        return _Ydn;
    }
    //Set D - Pad
    public float Get_D_PadHorizontal()
    {
        return _D_PadHorizontal;
    }
    public float Get_D_PadVertical()
    {
        return _D_PadVertical;
    }
    //Get Shoulder Buttons
    public bool Get_LeftBumper()
    {
        return _LeftBumper;
    }
    public bool Get_RightBumper()
    {
       return _RightBumper;
    }
    public float Get_LeftTrigger()
    {
        return _LeftTrigger;
    }
    public float Get_RightTrigger()
    {
        return _RightTrigger;
    }
    //Get Shoulder Buttons Dn
    public bool Get_LeftBumperDn()
    {
        return _LeftBumperDn;
    }
    public bool Get_RightBumperDn()
    {
        return _RightBumperDn;
    }
    //Get Joysticks
    public float Get_LeftJoystickHorizontal()
    {
        return _LeftJoystickHorizontal;
    }
    public float Get_LeftJoystickVertical()
    {
        return _LeftJoystickVertical;
    }
    public float Get_RightJoystickHorizontal()
    {
       return  _RightJoystickHorizontal;
    }
    public float Get_RightJoystickVertical()
    {
        return _RightJoystickVertical;
    }
    public bool Get_LeftJoystickClick()
    {
        return _LeftJoystickClick;
    }
    public bool Get_RightJoystickClick()
    {
        return _RightJoystickClick;
    }
    //Get Start And back buttons
    public bool Get_Start()
    {
        return _Start;
    }
    public bool Get_Back()
    {
        return _Back;
    }
}
