using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{

    public Rigidbody myRigidBody;
    public Camera myCamera;
    MovementScript myMovement;
    public Interactable myMenu;
    public ItemWheel myInventoryManager;
    public EquipmentInventory myInventory;
    public GameObject myWeapon;
    public GameObject myBody;
    public Text debugText;

    public enum PlayerStance
    {
        None,
        Standing,
        Crouching,
        Prone,
        //Grappling when grappling hook equiped and Active hook is out
    }
    [Header("Current States")]
    public PlayerStance myPlayerStance = PlayerStance.None;

    public enum PlayerState
    {
        None,
        Idle,
        Running,
        Sprinting,
        Sliding,
        Jumping,
        Falling,
        Death,
        //Swinging, when grappling and not on ground
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
        myMovement = GetComponent<MovementScript>();
        myMenu = GetComponent<Interactable>();
        SetPlayerState(PlayerState.Falling);
        SetPlayerStance(PlayerStance.Standing);
        myMovement.SetRunForce(myMovement.RunForce);
        myInventoryManager = GetComponent<ItemWheel>();
        myInventory = GetComponent<EquipmentInventory>();
    }

    void FixedUpdate()
    {

    }

    void Update()
    {
            DesiredDirection = new Vector3(
                    Input.GetAxis("Horizontal"),
                    0.0f,
                    Input.GetAxis("Vertical"));
            DesiredDirection.Normalize();

        if (myPlayerState == PlayerState.Sliding)
        {
            myMovement.MoveMe(myRigidBody, new Vector3(DesiredDirection.x, 0.0f, 0.0f));
        }
        else
        {
            if ((myPlayerState != PlayerState.Jumping) && (myPlayerState != PlayerState.Falling))
            {
                
                myMovement.HeadBob(myRigidBody, myCamera);
            }
            myMovement.MoveMe(myRigidBody, DesiredDirection);//the drag of the material I am moving on, 
            myMovement.BodyRotation(myCamera);
        }
        
        myMovement.MouseLook(myCamera);
        myMovement.AimWeapon(myCamera);
        PlayerStateChangeCode();
        PlayerStanceChangeCode();


        // check if firing weapon here
        if (Input.GetKeyDown("f"))
        {
            // open the weapon menu
            myInventoryManager.OpenMenu();
        }
        else if (Input.GetButtonDown("Fire1") && !Input.GetKey("f") && myInventory.equippedItemsList[0] != null)
        {
            if (myInventory.equippedItemsList[0].GetComponent<WeaponData>() != null)
            {
                myInventory.equippedItemsList[0].GetComponent<WeaponData>().Fire(myWeapon, myCamera, myInventory);
            }
            else if (myInventory.equippedItemsList[0].GetComponent<GrapplingHook>() != null)
            {
                
                myInventory.equippedItemsList[0].GetComponent<GrapplingHook>().FireHook(myWeapon);
            }
            
        }
        if (Input.GetButtonUp("Fire1") && myInventory.equippedItemsList[0] != null)
        {
            if (myInventory.equippedItemsList[0].GetComponent<GrapplingHook>() != null)
            {
                myInventory.equippedItemsList[0].GetComponent<GrapplingHook>().DestroyHook();
            }
        }
       

        // debug text stuff displayed on screen here
        debugText.text = "Debug Text"
            + "Desired Direction : " + DesiredDirection.ToString() + "\n"
           // + "RunSpeed         : " + MyMovement.GetCurrRunSpeed().ToString() + "\n"
           + "Player State        : " + GetPlayerState().ToString() + "\n"
           + "Player Stance       : " + GetPlayerStance().ToString() + "\n"
           + "Revolver Ammo       : " + myInventory.AmmoBackpack[1] + "/" + myInventory.AmmoBackpackMax[1] + "\n"
           + "Rifle Ammo       : " + myInventory.AmmoBackpack[2] + "/" + myInventory.AmmoBackpackMax[2] + "\n"
           + "Shotgun Ammo       : " + myInventory.AmmoBackpack[3] + "/" + myInventory.AmmoBackpackMax[3] + "\n";

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

    public virtual void PlayerStateChangeCode()
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
                        SetPlayerState(PlayerState.Running);
                    }
                    else
                    {
                        SetPlayerState(PlayerState.Sprinting);
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
            case PlayerState.Running:
                // if not moving go to idle state 
                if (myRigidBody.velocity == Vector3.zero)
                {
                    SetPlayerState(PlayerState.Idle);
                }
                // if moving while press shift go to sprint
                if (Input.GetKeyDown(KeyCode.LeftShift) || SprintToggle)
                {
                    SetPlayerState(PlayerState.Sprinting);
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
            case PlayerState.Sprinting:
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
                    SetPlayerState(PlayerState.Running);
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
                if (DesiredDirection.z < 0 && SecondsSinceSlide >.5 
                   || Mathf.Abs(myBody.transform.InverseTransformDirection(myRigidBody.velocity).x)
                    > myBody.transform.InverseTransformDirection(myRigidBody.velocity).z)
                {
                    SetPlayerState(PlayerState.Running);
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
                    SetPlayerState(PlayerState.Running);
                }
                if (IsOnGround() && DesiredDirection != Vector3.zero && SprintToggle)
                {
                    SetPlayerState(PlayerState.Sprinting);
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
    void SetPlayerState(PlayerState newState)
    {
        if (newState != myPlayerState)
        {
            myPlayerState = newState;
            switch (myPlayerState)
            {
                case PlayerState.Idle:
                    break;
                case PlayerState.Running:
                    if (myPlayerStance == PlayerStance.Standing)
                    {// standing set speed 75, if crouching set speed 50 
                        myMovement.SetRunForce(70);
                    }
                    if (myPlayerStance == PlayerStance.Crouching)
                    {
                        myMovement.SetRunForce(50);
                    }
                    break;
                case PlayerState.Sprinting:
                    myMovement.Sprint();
                    break;
                case PlayerState.Sliding:
                    SecondsSinceSlide = 0.0f;
                    myMovement.Slide(myRigidBody,this);
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
    public PlayerState GetPlayerState()
    {
        return myPlayerState;
    }

    public virtual void PlayerStanceChangeCode()
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
                    || myPlayerState == PlayerState.Sprinting)
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
    public virtual void SetPlayerStance(PlayerStance newStance)
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
    public PlayerStance GetPlayerStance()
    {
        return myPlayerStance;
    }

    public bool IsOnGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
            distanceToGround = hit.distance;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            distanceToGround = 1000f;
        }
        return distanceToGround <= 1.1f;
    }
    public bool IsFalling()
    {
        return (myRigidBody.velocity.y < -0.5f && !IsOnGround());
    }
    public bool IsUphill()
    {
        return (myRigidBody.velocity.y > 0.5f && IsOnGround());
    }
}
