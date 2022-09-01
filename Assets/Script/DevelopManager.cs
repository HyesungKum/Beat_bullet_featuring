using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopManager : MonoBehaviour
{
    private float timer;
    string[] note;
    private int count;
    bool doit = false;

    Transform popPosition;

    AudioSource music;

    [SerializeField] private float delay = 1f;

    // Start is called before the first frame update
    private void Awake()
    {
        popPosition = GameObject.Find("PopZone").GetComponent<Transform>();
        music = GetComponent<AudioSource>();
        music.clip = Resources.Load<AudioClip>("ImagineDragonsEnemy");

        count = 0;
        timer = 0f;
        LoadNoteDataFile();
    }
    // Update is called once per frame
    void Update()
    {
        if (count >= note.Length) return;

        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            doit = true;
            Invoke("MusicStart", delay);
        }

        if(doit)
        {
            LoadMSD();
        }

    }

    private void MusicStart()
    {
        music.Play();
    }
    private void LoadNoteDataFile()
    {
        TextAsset noteData = Resources.Load<TextAsset>("MSD");
        note = noteData.ToString().Split(",");
    }

    private void LoadMSD()
    {
        if (timer > ((60f / 142f) * 0.5f))
        {
            timer = 0f;

            if (note[count] == "M")
            {
                ObjectPool.Instance.PopObject(popPosition.position);    
            }

            count++;
        }
    }
}
