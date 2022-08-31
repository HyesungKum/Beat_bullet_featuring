using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioClip music;
    [SerializeField] public AudioClip metronom;

    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log("Hi");
            PlayMusic();
        }
    }

    private void PlayMusic()
    {
    }

    private void StopMusic()
    {
    }
}
