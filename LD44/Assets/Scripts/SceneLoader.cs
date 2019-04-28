using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    int sceneCount;
    int creditsScene;
    int currentScene;

    // Start is called before the first frame update
    void Start()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        creditsScene = sceneCount - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Scene Reload");
            ReloadScene();
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentScene + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(creditsScene);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void Quit()
    {
        Debug.Log("Application Quit!");
        Application.Quit();
    }

}
