using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    public string start = "Submit";
    Scene m_Scene;
    Scene m_OldScene;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        if(Input.GetButtonDown(start))
        {
            if(sceneName == "KoalaTrack" && Time.timeScale == 1)
            {
                print("Paws");
                Time.timeScale = 0;

            }
            else if (sceneName == "KoalaTrack" && Time.timeScale == 0)
            {
                print("Un-paws");
                Time.timeScale = 1;
            }
        }
    }

    public void LoadNewScene(string newScene)
    {
        m_OldScene = SceneManager.GetActiveScene();
        string oldSceneName = m_OldScene.name;
        print(oldSceneName);
        AsyncOperation op = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        op.completed += (AsyncOperation o) =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(newScene));
        };
        if (oldSceneName != "Preload")
        {
            SceneManager.UnloadSceneAsync(m_OldScene);
        }
    }

}
