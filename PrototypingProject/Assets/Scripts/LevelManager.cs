using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int team1Score;
    public int team2Score;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreIncreased(int teamValue)
    {
        if (teamValue == 1)
        {
            team2Score++;
        }
        else
        {
            team1Score++;
        }
    }

}
