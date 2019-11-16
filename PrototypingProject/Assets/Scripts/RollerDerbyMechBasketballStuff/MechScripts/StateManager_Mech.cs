using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager_Mech : StateManager
{

    //Rigidbody myRigidBody;
    //Camera myCamera;
    MovementScript_Mech myMovement;
    //Interactable myMenu;
    //public EquipmentInventory myInventory;
    //public ItemWheel myInventoryManager;
    //public GameObject myWeapon;
    //public GameObject myBody;
    //public Text debugText;
    
        /*
    public enum PlayerStance
    {
        None,
        Standing,
        Crouching,
        Prone,
    }
    [Header("Current States")]
    public PlayerStance myPlayerStance = PlayerStance.None;
    */

    //different states 
    public enum PlayerState
    {
        None,
        Idle,
        Manual, // manual steering mode
        Automatic, // automatic steering mode
        Sliding,
        Jetting,
        Jumping,
        Falling,
        Death,
        //add a using weapon wheel state 
    }
    public PlayerState myPlayerState = PlayerState.None;
    float secondsSinceOnGround = 0.0f;
    float distanceToGround = 0.0f;
    float SecondsSinceSlide = 0.0f;
    bool SprintToggle;
    Vector3 DesiredDirection;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myCamera = GetComponentInChildren<Camera>();
        myMovement = GetComponent<MovementScript_Mech>();
        myMenu = GetComponent<Interactable>();
        SetPlayerState(PlayerState.Falling);
        SetPlayerStance(PlayerStance.Standing);
        myMovement.SetRunForce(myMovement.ManualForce);
        myInventory = GetComponent<EquipmentInventory>();
        myInventoryManager = GetComponent<ItemWheel>();
    }

    void Update()
    {
            DesiredDirection = new Vector3(
                    Input.GetAxis("Horizontal"),
                    0.0f,
                    Input.GetAxis("Vertical"));
            DesiredDirection.Normalize();

        if (myPlayerState != PlayerState.Sliding)
        {
            
            myMovement.MoveMe(myRigidBody, DesiredDirection);
            myMovement.BodyRotation(myCamera,myPlayerState.ToString(),DesiredDirection);
            if ((myPlayerState != PlayerState.Jumping) && (myPlayerState != PlayerState.Falling))
            {
                myMovement.HeadBob(myRigidBody, myCamera);
            }
        }
        else
        {
            myMovement.MoveMe(myRigidBody, new Vector3(DesiredDirection.x,0.0f,0.0f));
        }
        myMovement.MouseLook(myCamera);
        myMovement.AimWeapon(myCamera);
        PlayerStateChangeCode();
        PlayerStanceChangeCode();

        if (Input.GetKeyDown("f"))
        {
            // open the weapon menu
            myInventoryManager.OpenMenu();
        }
        else if (Input.GetButtonDown("Fire1") && !Input.GetKey("f"))
        {
            if (myInventory.equippedItemsList[0].GetComponent<WeaponData>() != null)
            {
                myInventory.equippedItemsList[0].GetComponent<WeaponData>().Fire(myWeapon, myCamera, myInventory);
            }
            //myInventory.equippedItemsList[0].UseItem();
            //Debug.Log(myInventory.equippedItemsList[0].name);
        }

        debugText.text = "Debug Text" 
           //+ "Desired Direction : " + DesiredDirection.ToString() + "\n"
           //+ "RunSpeed         : " + myMovement.GetCurrRunSpeed().ToString() + "\n"
           + "Player Automatic enabled:" + SprintToggle + "\n"
           + "Player State        : " + GetPlayerState().ToString() + "\n"
           + "Player Stance       : " + GetPlayerStance().ToString() + "\n"
           + "Revolver Ammo       : " + myInventory.AmmoBackpack[1] + "/" + myInventory.AmmoBackpackMax[1] + "\n"
           + "Rifle Ammo       : " + myInventory.AmmoBackpack[2] + "/" + myInventory.AmmoBackpackMax[2] + "\n"
           + "Shotgun Ammo       : " + myInventory.AmmoBackpack[3] + "/" + myInventory.AmmoBackpackMax[3] + "\n"
           ;
        if (myInventory.equippedItemsList[0] != null)
        {
            if (myInventory.equippedItemsList[0].GetComponent<WeaponData>() != null)
            {
                debugText.text += myInventory.equippedItemsList[0].GetComponent<WeaponData>().title + "Weapon Ammo: " + myInventory.equippedItemsList[0].GetComponent<WeaponData>().ammo + "/" + myInventory.equippedItemsList[0].GetComponent<WeaponData>().ammoMax + "\n";
            }
            else
            {
                debugText.text += "Equipped Weapon Ammo: no Weapon equipped";
            }
        }
        else
        {
            debugText.text += "Equipped Weapon Ammo: no Weapon equipped";
        }
    }

    public override void PlayerStateChangeCode()
    {
        if (!IsOnGround())
        {
            secondsSinceOnGround += Time.deltaTime;
        }
        else
        {
            secondsSinceOnGround = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SprintToggle = !SprintToggle;
        }
        switch (myPlayerState)
        {
            case PlayerState.Idle:
                // if moving go to run state 
                if (myRigidBody.velocity != Vector3.zero && DesiredDirection != Vector3.zero)
                {
                    if (!SprintToggle)
                    {
                        SetPlayerState(PlayerState.Manual);
                    }
                    else
                    {
                        SetPlayerState(PlayerState.Automatic);
                    }
                }
                // if press jump go to jump state 
                if (Input.GetButtonDown("Jump"))
                {
                    SetPlayerState(PlayerState.Jumping);
                }
                // if falling go to falling state
                if (IsFalling())
                {
                    SetPlayerState(PlayerState.Falling);
                }
                break;
            case PlayerState.Manual:
                // if not moving go to idle state 
                if (myRigidBody.velocity == Vector3.zero)
                {
                    SetPlayerState(PlayerState.Idle);
                }
                // if moving while press shift go to sprint
                if (Input.GetKeyDown(KeyCode.LeftShift) || SprintToggle)
                {
                    SetPlayerState(PlayerState.Automatic);
                }
                // if press jump go to jump state 
                if (Input.GetButtonDown("Jump"))
                {
                    SetPlayerState(PlayerState.Jumping);
                }
                // if falling go to falling state 
                if (IsFalling())
                {
                    SetPlayerState(PlayerState.Falling);
                }
                break;
            case PlayerState.Automatic:
                // if not moving go to idle state 
                if (myRigidBody.velocity == Vector3.zero)
                {
                    SetPlayerState(PlayerState.Idle);
                }
                // if moving while holding shift go to sprint
                if (Input.GetKeyDown("c"))
                {
                    SetPlayerState(PlayerState.Sliding);
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    SetPlayerState(PlayerState.Manual);
                }
                // if press jump go to jump state 
                if (Input.GetButtonDown("Jump"))
                {
                    SetPlayerState(PlayerState.Jumping);
                }
                // if falling go to falling state 
                if (IsFalling())
                {
                    SetPlayerState(PlayerState.Falling);
                }
                break;
            case PlayerState.Sliding:
                SecondsSinceSlide += Time.deltaTime;
                if (myRigidBody.velocity == Vector3.zero)
                {
                    SetPlayerState(PlayerState.Idle);
                }
                if (!Input.GetKey("c")  && SecondsSinceSlide >.5)
                {
                    SetPlayerState(PlayerState.Manual);
                }
                if (IsFalling())
                {
                    SetPlayerState(PlayerState.Falling);
                }
                break;
            case PlayerState.Jumping:
                // if falling go to fall state 
                if (IsFalling())
                {
                    SetPlayerState(PlayerState.Falling);
                }
                break;
            case PlayerState.Falling:
                if (IsOnGround())
                {
                    SetPlayerState(PlayerState.Idle);
                }
                if (IsOnGround() && DesiredDirection!= Vector3.zero)
                {
                    SetPlayerState(PlayerState.Manual);
                }
                if (IsOnGround() && DesiredDirection != Vector3.zero && SprintToggle)
                {
                    SetPlayerState(PlayerState.Automatic);
                }
                if (IsOnGround() && DesiredDirection != Vector3.zero && SprintToggle && myPlayerStance == PlayerStance.Crouching)
                {
                    SetPlayerState(PlayerState.Sliding);
                }
                break;
                
            case PlayerState.Death:
                break;
        }
    }
    void SetPlayerState(PlayerState newState) // not an override because playerstats is hiding playerstate.base
    {
        if (newState != myPlayerState)
        {
            myPlayerState = newState;
            switch (myPlayerState)
            {
                case PlayerState.Idle:
                    break;
                case PlayerState.Manual:
                    if (myPlayerStance == PlayerStance.Standing)
                    {// standing set run force 70, if crouching set run force 50 
                        myMovement.SetRunForce(70);
                    }
                    if (myPlayerStance == PlayerStance.Crouching)
                    {
                        myMovement.SetRunForce(50);
                    }
                    break;
                case PlayerState.Automatic:
                    myMovement.Sprint();
                    break;
                case PlayerState.Sliding:
                    SecondsSinceSlide = 0.0f;
                    myMovement.Slide(myRigidBody,this,DesiredDirection);
                    break;
                case PlayerState.Jumping:
                    // jump on entering the jump state 
                    myMovement.Jump(myRigidBody);
                    break;
                case PlayerState.Falling:
                    break;
                case PlayerState.Death:
                    break;

            }
        }
    }


    public override void PlayerStanceChangeCode()
    {
        switch (myPlayerStance)
        {
            case PlayerStance.Standing:
                if (Input.GetKeyDown("c"))
                {
                    SetPlayerStance(PlayerStance.Crouching);
                }
                break;
            case PlayerStance.Crouching:
                if (((Input.GetButtonDown("Jump")
                    || Input.GetKeyDown(KeyCode.LeftShift)
                    || Input.GetKeyUp("c"))
                    && myPlayerState != PlayerState.Sliding)
                    || myPlayerState != PlayerState.Sliding
                    && !Input.GetKey("c")
                    || myPlayerState == PlayerState.Automatic)
                {
                    SetPlayerStance(PlayerStance.Standing);
                }
                break;
            case PlayerStance.Prone:
                if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown("c"))
                {
                    SetPlayerStance(PlayerStance.Standing);
                }
                break;
        }
    }
    public override void SetPlayerStance(PlayerStance newStance)
    {
        if (newStance != myPlayerStance)
        {
            myPlayerStance = newStance;
            switch (myPlayerStance)
            {
                case PlayerStance.Standing:
                    myMovement.Stand(this);
                    break;
                case PlayerStance.Crouching:
                    myMovement.Crouch(this);
                    break;
                case PlayerStance.Prone:
                    break;
            }
        }
    }
}
