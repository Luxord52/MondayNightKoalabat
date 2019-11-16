using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    public string start = "Submit";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton(start))
        {
            print("SEVEN");
        }
    }

    IEnumerator AsynchronousLoad(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (ao.progress == 0.9f)
            {
                Debug.Log("Press Start/Enter to start");
                if (Input.GetButton(start))
                    ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void LoadNewScene(string Scene)
    {
        if (Scene == "KoalaPrep")
        {
            AsynchronousLoad("KoalaTrack");
            SceneManager.LoadScene(Scene);
        }
        else
        {
            SceneManager.LoadScene(Scene);
        }
    }

}
