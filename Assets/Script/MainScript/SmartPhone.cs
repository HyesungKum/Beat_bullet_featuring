using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPhone : MonoBehaviour
{
    //components
    AudioSource source;

    List<AudioClip> audioClips;

    AudioClip turntableClip;

    GameObject[] phoneWindow;

    //variableValue
    int musicIndex;

    private void Awake()
    {
        audioClips = new List<AudioClip>();
        source = GetComponent<AudioSource>();
        turntableClip = Resources.Load<AudioClip>("TurnTableScratch");

        audioClips.Add(Resources.Load<AudioClip>("EnemySample"));
        audioClips.Add(Resources.Load<AudioClip>("BadGuySample"));

        phoneWindow = new GameObject[audioClips.Count];
    }

    private void Start()
    {
        musicIndex = 0;

        for (int i = 0; i < audioClips.Count; i++)
        {
            phoneWindow[i] = transform.GetChild(i).gameObject;
        }
        phoneWindow[1].SetActive(false);
        this.transform.GetComponent<AudioSource>().PlayOneShot(audioClips[musicIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.SelectMusicSquence) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))//Next Music
        {
            NextMusic();
            TurnOnPhoneWindow();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevMusic();
            TurnOnPhoneWindow();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.SelectMusicSquence = false;
            GameManager.Instance.MusicIndex = musicIndex;
            StartCoroutine(MovePhone());
        }
    }
    void TurnOnPhoneWindow()
    {
        for (int i = 0; i < phoneWindow.Length; i++)
        {
            phoneWindow[i].SetActive(false);
        }

        phoneWindow[musicIndex].SetActive(true);
    }
    IEnumerator MovePhone()
    {
        source.Stop();
        PlayTurnTable();

        Vector3 DesirePosion = new Vector3(this.transform.position.x, -1000f, this.transform.position.z);
        float timer = 0f;

        while (true)
        {
            timer += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, DesirePosion , Time.deltaTime);

            if (timer > 2f)
            {
                gameObject.SetActive(false);
            }

            yield return null;
        }
    }
    void NextMusic()
    {
        PlayTurnTable();
        musicIndex++;
        if (musicIndex >= audioClips.Count)
        {
            musicIndex = 0;
        }
        this.transform.GetComponent<AudioSource>().PlayOneShot(audioClips[musicIndex]);


    }
    void PrevMusic()
    {
        PlayTurnTable();
        musicIndex--;
        if (musicIndex < 0)
        {
            musicIndex = audioClips.Count-1;
        }
        this.transform.GetComponent<AudioSource>().PlayOneShot(audioClips[musicIndex]);

    }
    void PlayTurnTable()
    {
        this.transform.GetComponent<AudioSource>().Stop();
        this.transform.GetComponent<AudioSource>().PlayOneShot(turntableClip);
    }
}
