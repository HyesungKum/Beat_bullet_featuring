using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNoteManager : MonoBehaviour
{
    private float timer;
    string[] note;
    private int count;
    bool doit = false;

    Transform popPositionR;
    Transform popPositionL;

    AudioSource music;

    GameObject RMPrefab;
    GameObject LMPrefab;
    GameObject RDPrefab;
    GameObject LDPrefab;

    [SerializeField] private float delay = 2f;

    //
    bool onetimeFlag = true;

    [SerializeField] bool LoadMode = true;

    // Start is called before the first frame update
    private void Awake()
    {
        popPositionR = GameObject.Find("PopZoneR").GetComponent<Transform>();
        popPositionL = GameObject.Find("PopZoneL").GetComponent<Transform>();
        music = GetComponent<AudioSource>();
        music.clip = Resources.Load<AudioClip>("ImagineDragonsEnemy");

        RMPrefab = Resources.Load<GameObject>("MeleeNoteR");
        LMPrefab = Resources.Load<GameObject>("MeleeNoteL");
        RDPrefab = Resources.Load<GameObject>("DodgeNoteR");
        LDPrefab = Resources.Load<GameObject>("DodgeNoteL");

        if (LoadMode)
        {
            count = 0;
            timer = 0f;
            LoadNoteDataFile();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (LoadMode)
        {
            if (GameManager.Instance.SelectMusicSquence) return;


            if (count >= note.Length) return;

            timer += Time.deltaTime;

            LoadMSD();

            if (onetimeFlag)
            {
                Invoke("MusicStart", delay);
                //Invoke("DrumStart", delay);
                onetimeFlag = false;
            }
        }
        else
        {
            if (GameManager.Instance.SelectMusicSquence) return;

            if (onetimeFlag)
            {
                MusicStart();
                //DrumStart();
                onetimeFlag = false;
            }
        }
    }

    private void MusicStart()
    {
        music.clip = Resources.Load<AudioClip>("ImagineDragonsEnemy");
        music.Play();
    }
    private void DrumStart()
    {
        AudioSource drumAudio = GameObject.Find("DrumAudio").GetComponent<AudioSource>();
        drumAudio.clip = Resources.Load<AudioClip>("EnemyDurm");
        drumAudio.Play();
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
            if (note[count] == "RM")
            {
                ObjectPool.Instance.PopObject(popPositionR.position, RMPrefab, "meleeNote");    
            }
            else if(note[count] == "LM")
            {
                ObjectPool.Instance.PopObject(popPositionL.position, LMPrefab, "meleeNote");
            }
            else if (note[count] == "LD")
            {
                ObjectPool.Instance.PopObject(popPositionL.position, LDPrefab, "meleeNote");
            }
            else if (note[count] == "RD")
            {
                ObjectPool.Instance.PopObject(popPositionR.position, RDPrefab, "meleeNote");
            }
            else if (note[count] == "D")
            {
                ObjectPool.Instance.PopObject(popPositionR.position, RDPrefab, "meleeNote");
            }

            count++;
        }
    }
}
