using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleTon<GameManager>
{
    //constant value
    [SerializeField] float maxHealth = 100f;

    //variable value
    public float Health { get; set; }
    public int Score    { get; set; }
    public int Combo    { get; set; }
    public bool Pause   { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Combo);
    }

    private void Init()
    {
        Health = maxHealth;
        Score = 0;
        Combo = 0;
        Pause = false;
    }
}
