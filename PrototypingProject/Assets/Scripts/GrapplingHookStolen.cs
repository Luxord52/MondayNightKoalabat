using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookStolen : MonoBehaviour
{
    //Object Type to place at the grapple point
    public GameObject grapplingHookPrefab;
    //Variable to store the instance ID of our current Grapple Point
    public GameObject currentGrapplingHook;
    //Missed grappling hooks
    public GameObject failedGrapplePrefab;
    //Variable to store the last missed grappling hook
    public GameObject failedGrapple;

    //LocationInfo of THIS
    private Vector3 grapplePoint = new Vector3(0, 0, 0);

    //grapple variables 
    public bool grappleDeployed = false;
    private float grappleRetractSpeed = 10f;
    private float grapplePayoutSpeed = 10f;
    private float ropeLength;
    private Vector3 ropeTension = Vector3.zero;
    private Vector3 directionToGrapple;
    public Vector3 customGravity;
    private int retracting;
    public Rigidbody rigidBody;

    private bool clambering = false;
    private float clamberTolerance = 1f;
    private Vector3 clamberPoint = Vector3.zero;

    //returns the world coorinated corresponding ot the cursor position at z=0
    public Vector3 CursorPosition()
    {
        //draw a line from the camera to the cursor position
        Ray rayFromCameraToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Create a plane with a normal pointing towards the camera, which goes through the origin.
        Plane zEqualsZero = new Plane(new Vector3(0, 0, -1), new Vector3(0, 0, 0));

        //Set up a variable to store how far along a ray the z-0 plane is
        float distanceFromCameraToZEqualsZero = 0f;

        //use a function of this plane to set that variable to the distance along the ray it intersects
        zEqualsZero.Raycast(rayFromCameraToMouse, out distanceFromCameraToZEqualsZero);

        //return position given by following our ray by the distance we just found
        return rayFromCameraToMouse.GetPoint(distanceFromCameraToZEqualsZero);
    }

    public void SetRopeLength()
    {
        ropeLength = Vector3.Distance(grapplePoint, transform.position);
    }

    //do all physicsy stuff in fixed update
    void FixedUpdate()
    {
        //calculate how much gravity should pull us this frame
        customGravity = Physics.gravity * 60; // what actual variable should we be useing here instead of 60. Time.fixedDeltaTime is 1/50, so it doesnt work.

        //vector from our position to the grapple point
        Vector3 vectorToGrapple = grapplePoint - transform.position;

        // Scalar distance from us to grapple
        float distanceToGrapple = vectorToGrapple.magnitude;

        //unit vector of the direction from us to grapple 
        directionToGrapple = vectorToGrapple / distanceToGrapple;

        //how fast is our velocity in the direction of the anchor point?
        float speedTowardsAnchor = Vector3.Dot(rigidBody.velocity, directionToGrapple);

        if (grappleDeployed)
        {
            //Rope counteracts the pulling away from anchor component of gravity
            AddTension();

            if (retracting == 0)
            {
                if (speedTowardsAnchor != 0)
                {
                    // add that much velocity in the other direction
                    rigidBody.velocity = rigidBody.velocity - (speedTowardsAnchor * directionToGrapple);
                }
            }
            else if (retracting == 1)
            {
                //pulling towards grapple
                if (speedTowardsAnchor != grappleRetractSpeed)
                {
                    //neutralise grapplewards Gravity
                    rigidBody.velocity = rigidBody.velocity - (speedTowardsAnchor * directionToGrapple);

                    //make it grapple retract speed 
                    rigidBody.velocity = rigidBody.velocity + (grappleRetractSpeed * directionToGrapple);
                }

                //updtate rope length so we dont fall back down
                // positive because if we're moving towards, vTA will be positive and we want the rope to get shorter
                // Always by retractspeed, even if we were moving faster - the rope will still shorten at the same rate
                ropeLength = ropeLength - (grappleRetractSpeed * Time.deltaTime);

                if (clambering && ropeLength < clamberTolerance)
                {
                    Vector3 clamberOffset = new Vector3(0, 0, 0);

                    if (Physics.Raycast(clamberPoint, new Vector3(-1, 0, 0), 1))
                    {
                        clamberOffset = new Vector3(-1, 2, 0);
                    }
                    else if (Physics.Raycast(clamberPoint, new Vector3(1, 0, 0), 1))
                    {
                        clamberOffset = new Vector3(1, 2, 0);
                    }
                    else
                    {
                        clambering = false;
                    }
                    if (clambering)
                    {
                        //shift him up to our roof
                        transform.position = clamberPoint + clamberOffset;
                        //Upright
                        transform.rotation = Quaternion.identity;
                        //stationary
                        rigidBody.velocity = Vector3.zero;
                        //detach him
                        Ungrapple();

                        clambering = false;
                    }
                }
            }
            else if (retracting == -1)
            {// paying out rope
                if (speedTowardsAnchor != grapplePayoutSpeed * -1)
                {
                    //neutralize grapplewards velocity
                    rigidBody.velocity = rigidBody.velocity - (speedTowardsAnchor * directionToGrapple);

                    //make it grapple retract speed
                    rigidBody.velocity = rigidBody.velocity - (grapplePayoutSpeed * directionToGrapple);
                }

                // minus because if were moving away vta will be negative, and we want rope length to increase 
                ropeLength = ropeLength + (grapplePayoutSpeed * Time.deltaTime);
            }
            //Debug.Log ("Rope Length:" + ropeLength + "Distance to grapple: " + distanceToGrapple);

            //Debug.Log("Needed: "+ (GrappleRetractSpeed - SpeedTorwardsAnchor) + "Speed to anchor: " + speedTowardsAnchor);

        }// grappleDeployed
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ClamberTrigger") && grappleDeployed)
        {
            clambering = true;
            clamberPoint = other.transform.position;
        }
    }
    void onTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ClamberTrigger") && grappleDeployed)
        {
            clambering = false;
        }
    }
    void AddTension()
    {
        // add the tension of the rope how hard gravity is pulling away from the anchor // line 192
        ropeTension = directionToGrapple * Vector3.Dot(customGravity, directionToGrapple) * -1;

        // if gravity is pulling away from the grapple its anchorwards component is negative so we multiply it by the -1 because we want a positive in
        rigidBody.AddForce(ropeTension);
    }
    void Ungrapple()
    {
        //destroy the last grapple point
        Destroy(currentGrapplingHook);

        grappleDeployed = false;
    }
    void Update()
    {
        if (currentGrapplingHook == null)
        {
            // our grappling hook was destroyed somehow
            grappleDeployed = false;
        }
        //shooting the grapple and disconnecting
        if (Input.GetButtonDown("Fire1"))
        {
            //always destroy and failed grapples whether were attached or not
            Destroy(failedGrapple);
            if (grappleDeployed)
            {
                //disconnect
                Ungrapple();
            }
            else
            {
                //fire grapple
                RaycastHit grappleHit;

                if (Physics.Raycast(transform.position, CursorPosition() - transform.position, out grappleHit))
                {
                    // we hit something

                    //grapple onto first thing we hit
                    grapplePoint = grappleHit.point;

                    //Create the hook there, and remember it to be deleted later
                    currentGrapplingHook = Instantiate(grapplingHookPrefab, grapplePoint, transform.rotation) as GameObject;

                    //note how long the rope should be 
                    SetRopeLength();

                    //remember we're swinging now
                    grappleDeployed = true;

                }
                else
                {
                    //we hit nothing, fire out anyways
                    failedGrapple = Instantiate(failedGrapplePrefab, CursorPosition(), transform.rotation) as GameObject;
                }
            }
        }
        if (grappleDeployed)
        {
            if (Input.GetButton("Up") && !Input.GetButton("Down"))
            {
                //retracting
                retracting = 1;
            }
            else if (Input.GetButton("Down") && !Input.GetButton("Up"))
            {
                //Paying out 
                retracting = -1;
            }
            else
            {
                retracting = 0;
            }
        }
    }

}
