using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class UIManager : MonoBehaviour
{
    private AudioSource audio;
    float[] spectrum = new float[64];

    RectTransform[] Bars = new RectTransform[7];

    Transform popPositionR;
    Transform popPositionL;

    StringBuilder MSD;

    GameObject prefab;

    float timer = 0f;
    bool started = false;

    void Start()
    {
        audio = GameObject.Find("LoadNoteManager").GetComponent<AudioSource>();
        MSD = new StringBuilder();
        popPositionR = GameObject.Find("PopZoneR").GetComponent<Transform>();
        popPositionL = GameObject.Find("PopZoneL").GetComponent<Transform>();

        prefab = Resources.Load<GameObject>("MeleeNote");

        Bars[0] = GameObject.Find("Bar1").GetComponent<RectTransform>();
        Bars[1] = GameObject.Find("Bar2").GetComponent<RectTransform>();
        Bars[2] = GameObject.Find("Bar3").GetComponent<RectTransform>();
        Bars[3] = GameObject.Find("Bar4").GetComponent<RectTransform>();
        Bars[4] = GameObject.Find("Bar5").GetComponent<RectTransform>();
        Bars[5] = GameObject.Find("Bar6").GetComponent<RectTransform>();
        Bars[6] = GameObject.Find("Bar7").GetComponent<RectTransform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        FFTBar();
        //Recording();
    }

    private void FFTBar()
    {
        audio.GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 0; i < Bars.Length; i++)
        {
            Bars[i].sizeDelta = Vector2.Lerp(Bars[i].sizeDelta, new Vector2(20f, spectrum[i * 10 + 1] * ((i * 2 + 1) * 1000)), 0.1f);
        }
    }
    
    private void Recording()
    {
        if (audio.GetComponent<AudioSource>().isPlaying)
        {
            started = true;

            if (timer > (60f / 142f) * 0.5f)
            {
                timer = 0;
                //GameObject.find
                if (Bars[2].sizeDelta.y > 30f)//average
                {
                    if (Random.Range(0, 3) > 1)
                    {
                        MSD.Append("RM,");
                        ObjectPool.Instance.PopObject(popPositionR.position, prefab, "meleeNote");
                    }
                    else
                    {
                        MSD.Append("LM,");
                        ObjectPool.Instance.PopObject(popPositionL.position, prefab, "meleeNote");
                    }

                }
                else if (Bars[4].sizeDelta.y > 35f)//average
                {
                    if (Random.Range(0, 2) > 1)
                    {
                        MSD.Append("RD,");
                        ObjectPool.Instance.PopObject(popPositionR.position, prefab, "meleeNote");
                    }
                    else
                    {
                        MSD.Append("LD,");
                        ObjectPool.Instance.PopObject(popPositionR.position, prefab, "meleeNote");
                    }
                }
                else if (Bars[6].sizeDelta.y > 20f)
                {
                    MSD.Append("D,");
                    ObjectPool.Instance.PopObject(popPositionR.position, prefab, "meleeNote");
                }
                else
                {
                    MSD.Append("N,");
                }
            }
        }
        else if (!audio.GetComponent<AudioSource>().isPlaying && started)
        {
            started = false;
            string path = @"C:\Users\User\Desktop\Unity_project\Beat_bullet_featuring\Assets\Resources\MSD.csv";
            File.WriteAllText(path, MSD.ToString());
        }
    }
}
