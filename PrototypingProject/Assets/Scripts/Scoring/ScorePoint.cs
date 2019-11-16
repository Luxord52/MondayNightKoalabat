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

    private void OnCollisionEnter(Collision collision)
    {
        IncreaseScore(collision.gameObject);
    }

    public void IncreaseScore(GameObject player)
    {
        StateManager playerStateManager = player.GetComponent<StateManager>();
        int teamValue = playerStateManager.team;
        levelManager.ScoreIncreased(teamValue);
    }
}
