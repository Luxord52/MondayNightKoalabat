using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYFollowCameraScript : MonoBehaviour {

    public GameObject FollowTarget;
    public Vector2 FollowOffset = Vector2.zero;
    public float FollowSpeed = 8.0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        FollowScript();
    }
    void FollowScript()
    {

        float targetX = FollowTarget.transform.position.x + FollowOffset.x;
        float targetY = FollowTarget.transform.position.y + FollowOffset.y;

        float currentX = transform.position.x;
        float currentY = transform.position.y;
        currentX += (targetX - currentX) * FollowSpeed * Time.deltaTime;
        currentY += (targetY - currentY) * FollowSpeed * Time.deltaTime;

        transform.position = new Vector3(
            currentX,
            currentY,
            transform.position.z
            );
    }
    public void SetFollowTarget(GameObject obj)
    {
        FollowTarget = obj;
    }
}
