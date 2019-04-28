using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishController : MonoBehaviour
{

    [SerializeField] float audioVolume = 0.5f;

    SceneLoader sceneLoader;
    AudioSource myAudio;
 
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        myAudio = GetComponent<AudioSource>();

        myAudio.volume = audioVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Level Completed!");
            myAudio.PlayOneShot(myAudio.clip);
            Invoke("WinLevel", 2f);
        }
    }

    private void WinLevel()
    {
        sceneLoader.LoadNextScene();
    }
}
