using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioClip sampleMusic1;
    [SerializeField] public AudioClip sampleMusic2;
    [SerializeField] public AudioClip turnTableSkrech;
    [SerializeField] public AudioClip metronom;

    private AudioSource audioSource;
    private List<AudioClip> sampleMusicList;

    int index = 0;

    bool flag = true;

    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        sampleMusicList = new List<AudioClip>();

        sampleMusicList.Add(sampleMusic1);
        sampleMusicList.Add(sampleMusic2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            audioSource.Stop();
            audioSource.PlayOneShot(turnTableSkrech);

            if (flag)
            {
                flag = false;
            }

            GameManager.Instance.PopStart = true;

            index++;
            if (index > 1)
            {
                index = 0;
            }
            Invoke("MusicChange", 1.17f);
            
        }


        if (GameManager.Instance.Pause)
        {
            StopMusic();
        }
        else
        {
            PlayMusic();
        }
    }

    private void MusicChange()
    {
        audioSource.PlayOneShot(sampleMusicList[index]);
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
