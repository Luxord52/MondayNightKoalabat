using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILogic : MonoBehaviour
{
    public bool readyQueryShown = false;
    private bool queryAlreadyShown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (readyQueryShown && queryAlreadyShown != true)
        {
            print("yay");
            queryAlreadyShown = true;
        }
    }

    public void ShowReadyQuery()
    {
        readyQueryShown = true;
    }

    public void HideReadyQuery()
    {
        readyQueryShown = false;
        queryAlreadyShown = false;
    }
}
