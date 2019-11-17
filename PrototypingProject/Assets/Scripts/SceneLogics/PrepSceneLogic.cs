using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepSceneLogic : MonoBehaviour
{
    GameObject gameMegaboss;
    SceneLogic sceneLogic;
    UILogic uiLogic;
    public bool playersReady = false;

    // Start is called before the first frame update
    void Start()
    {
        gameMegaboss = GameObject.FindGameObjectWithTag("GameMegaboss");
        sceneLogic = gameMegaboss.GetComponent<SceneLogic>();
        uiLogic = gameMegaboss.GetComponent<UILogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playersReady)
        {
            ReadyToStart();
        }
        else
        {
            uiLogic.HideReadyQuery();
        }
    }

    private void ReadyToStart()
    {
        uiLogic.ShowReadyQuery();
        if (Input.GetButtonDown(sceneLogic.start))
        {
            sceneLogic.LoadNewScene("KoalaTrack");
        }
    }
}
