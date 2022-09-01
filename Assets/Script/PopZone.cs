using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopZone : MonoBehaviour
{
    //constant value
    [SerializeField] private float BPM = 142f;
    [SerializeField] private float tempo1 = 4;
    [SerializeField] private float tempo2 = 4;
    private float popTime = 0f;

    [SerializeField] public TextAsset SheetMusicNote;

    //variable value
    private float timer;

    // Start is called before the first frame update
    private void Awake()
    {
        popTime = (60f / BPM) * (tempo1 / tempo2);
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!GameManager.Instance.PopStart) return;
        //
        //timer += Time.deltaTime;
        //
        //if (timer > popTime)
        //{
        //    timer = 0f;
        //    ObjectPool.Instance.PopObject(transform.position);
        //}
    }
}
