using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : ItemData
{
    
    // gun variables 
    public float range; // maximum range of shooting raycast
    //Tether Variables
    public float ropeLength;
    public float tetherSpeed;
    public Vector3 tetherPoint;
    Rigidbody myRigidBody;
    public bool grappling;

    float speedTowardsAnchor;
    private Vector3 directionToGrapple;
    private Vector3 ropeTension;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = FindObjectOfType<StateManager>().gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // update physics moving bits here 
            //vector from our position to the grapple point
            Vector3 vectorToGrapple = tetherPoint - myRigidBody.transform.position;

            //unit vector of the direction from us to grapple 
            directionToGrapple = vectorToGrapple.normalized;
        
        // if grappling and the player can see their teather point
        if (grappling == true && TetherVisible(myRigidBody, tetherPoint) && tetherPoint != Vector3.zero)
            {
                TetherMovement(myRigidBody, tetherPoint);
            }
    }

    public override void UseItem(Interactable itemWheel, int index)
    {

        // called by the weapon wheel when an item is selected.
        if (index < itemWheel.myInventory.inventoryList.Count)
        {
            if (itemWheel.myInventory.equippedItemsList.Capacity <= 0)
            {
                itemWheel.myInventory.equippedItemsList.Add(itemWheel.myInventory.inventoryList[index]);
                //itemWheel.myInventory.ItemModelList[index].SetActive(true);
            }
            else
            {
                itemWheel.myInventory.equippedItemsList[0] = itemWheel.myInventory.inventoryList[index];
                //itemWheel.myInventory.ItemModelList[index].SetActive(true);
            }

        }
        else
        {
            Debug.Log("Attempted to Equip Null inventory slot");
        }

    }

    public override void ItemModelVisible(Interactable myInteractable, int index)
    {
        if (myInteractable.myInventory.inventoryListSlotRestriction.Length > 0)
        {
            for (int i = 0; i < myInteractable.myInventory.inventoryListSlotRestriction.Length; i++)
            {
                if (myInteractable.myInventory.ItemModelList[i] != null)
                {
                    myInteractable.myInventory.ItemModelList[i].SetActive(false);
                    //Debug.Log("Ping"+i);
                }
            }
        }
        else
        {
            for (int i = 0; i < myInteractable.myInventory.ItemModelList.Count; i++)
            {
                myInteractable.myInventory.ItemModelList[i].SetActive(false);
            }
        }
        myInteractable.myInventory.ItemModelList[index].SetActive(true);
        Debug.Log(myInteractable.myInventory.equippedItemsList[0].name);
    }

    public override void ItemModelDestroy(Interactable myInteractable, int index)
    {
        Destroy(myInteractable.myInventory.ItemModelList[index]);
    }

    public void FireHook(GameObject myWeapon)
    {
        RaycastHit hit;
        if (Physics.Raycast(myWeapon.transform.position, myWeapon.transform.forward, out hit, range))
        {
            tetherPoint = hit.point;
            grappling = true;
        }
    }

    public void DestroyHook()
    {
        grappling = false;
        tetherPoint = Vector3.zero;
    }

    public void TetherMovement(Rigidbody myRigidBody, Vector3 TetherPoint)
    {
        //Debug.DrawRay(myRigidBody.transform.position, myRigidBody.velocity, Color.cyan);
        float distance = Vector3.Distance(myRigidBody.position, TetherPoint);
        if (distance > ropeLength)
        {
            Vector3 velocityDump = myRigidBody.velocity;

            // Set the players position to inside of the Teather
            myRigidBody.position += directionToGrapple * (distance - ropeLength);

            //Set the Players velocity
            myRigidBody.velocity = Vector3.ProjectOnPlane(velocityDump, directionToGrapple);

        }
    }

    public bool TetherVisible( Rigidbody myRigidBody, Vector3 TetherPoint)
    {
        int layerMask1 = 1 << 8;
        int layerMask2 = 1 << 0;
        int FinalLayerMask = layerMask1 | layerMask2;


        RaycastHit hit; 
        // if my rigid body can SEE the tetherpoint
        if (
            Physics.Raycast(
            myRigidBody.transform.position,
            directionToGrapple,
            out hit,
            Mathf.Infinity,
            FinalLayerMask)
            )
        {
            if ((TetherPoint - hit.point).magnitude <= 1) // if I can see the tether
            {
                Debug.DrawRay(myRigidBody.transform.position, (TetherPoint - myRigidBody.transform.position), Color.blue);
                return true;
            }
            else
            {

                Debug.Log("Cannot See Tether");
                return false;
                // Break the Tether or create new tether point?
            }
        }
        else
        {
            return false;
        }
    }
}
