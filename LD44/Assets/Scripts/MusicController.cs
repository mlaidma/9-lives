using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [SerializeField] AudioClip[] meowClips;
    [SerializeField] float meowVolume = 0.5f;

    AudioSource audioSource;

    bool started = false;
    int meowClipCount;
    private bool randomMeowEnabled = true;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartBackgroundMusic", 5f);
        meowClipCount = meowClips.Length;
        Invoke("PlayMeow", 10f);
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void StartBackgroundMusic()
    {
        audioSource.Play();
    }

    private void PlayMeow()
    {
        if(randomMeowEnabled)
        {
            int clipIndex = UnityEngine.Random.Range(0, meowClipCount);
            float nextClipInterval = UnityEngine.Random.Range(15f, 20f);

            audioSource.PlayOneShot(meowClips[clipIndex], meowVolume);
            Invoke("PlayMeow", nextClipInterval);
        }
    }
}
