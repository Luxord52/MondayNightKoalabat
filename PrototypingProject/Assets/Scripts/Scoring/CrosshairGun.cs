using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairGun : MonoBehaviour
{
    public RectTransform crosshair;
    public Camera cam;
    public Transform muzzle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit))
        {
            if (hit.collider)
            {
                crosshair.position = cam.WorldToScreenPoint(hit.point);
            }
        }
    }
}
