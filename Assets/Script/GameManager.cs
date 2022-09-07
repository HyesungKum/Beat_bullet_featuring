using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleTon<GameManager>
{
    //components

    //constant value
    [SerializeField] float maxHealth = 100f;

    //variable value
    public float Health { get; set; }
    public float Volum { get; set; }
    public float SyncDelay { get; set; }
    public int MusicIndex { get; set; }
    public int Score    { get; set; }
    public int Combo    { get; set; }
    public bool Pause   { get; set; }
    public bool PopStart { get; set; }
    public bool SelectMusicSquence { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Init();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Init()
    {
        Health = maxHealth;
        Score = 0;
        Combo = 0;
        Pause = false;
        PopStart = false;
        SelectMusicSquence = true;
    }
}
