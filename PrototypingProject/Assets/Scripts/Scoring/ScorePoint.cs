using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    public int scoreThreshold;
    public GameObject levelManagerObject;
    LevelManager levelManager;


    // Start is called before the first frame update
    void Start()
    {
        levelManagerObject = GameObject.FindWithTag("LevelManager");
        levelManager = levelManagerObject.GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        
    }

    /*private void OnTriggerEnter(Collision collision)
    {
        IncreaseScore(collision.gameObject);
    }*/
    private void OnTriggerEnter(Collider other)
    {
        IncreaseScore(other.gameObject);
    }
    public void IncreaseScore(GameObject player)
    {
        StateManager playerStateManager = player.GetComponent<StateManager>();
        int teamValue = playerStateManager.team;
        if (playerStateManager.highestZone == scoreThreshold)
        {
            levelManager.ScoreIncreased(teamValue);
            playerStateManager.ResetZones();
        }
    }
}
