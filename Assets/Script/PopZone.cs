using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopZone : MonoBehaviour
{
    //constant value
    [SerializeField] private float BPM = 142f;
    private float popTime = 0f;

    [SerializeField] public TextAsset SheetMusicNote;

    //variable value
    private float timer;

    // Start is called before the first frame update
    private void Awake()
    {
        popTime = 60f / BPM;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > popTime)
        {
            timer = 0f;
            ObjectPool.Instance.PopObject(transform.position);
        }
    }
}
