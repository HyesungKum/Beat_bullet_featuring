using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.Pause)
        {
            StopMusic();
        }
        else
        {
            PlayMusic();
        }
    }
    private void PlayMusic()
    {
        audioSource.Play();
    }

    private void StopMusic()
    {
        audioSource.Stop();
    }

}
