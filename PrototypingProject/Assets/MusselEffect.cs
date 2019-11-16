using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusselEffect : MonoBehaviour
{
    public LineRenderer myLine;
    public float lifeTime;
    public bool ShowDecay;
    float count;
    public Vector3 StartPos;
    public Vector3 EndPos;

    public void SetPositions(Vector3 start, Vector3 end)
    {
        StartPos = start;
        EndPos = end;
        myLine.SetPosition(0, StartPos);
        myLine.SetPosition(1, EndPos);
    }

    public void LineDecay()
    {
        
        
            StartPos = Vector3.Lerp(StartPos, EndPos, 1 - lifeTime);
            myLine.SetPosition(0, StartPos);
            Vector3 dump = EndPos - StartPos;
            if (dump.magnitude <= 1)
            {
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LineDecay();
    }
}
