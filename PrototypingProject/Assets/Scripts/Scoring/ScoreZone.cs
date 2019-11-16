using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    public int zoneNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        print("Enter Collision");
        IncreaseZone(collision.gameObject);
    }

    public void IncreaseZone(GameObject player)
    {
        StateManager playerStateManager = player.GetComponent<StateManager>();
        playerStateManager.oldZone = playerStateManager.newZone;
        playerStateManager.newZone = zoneNumber;
    }
}
