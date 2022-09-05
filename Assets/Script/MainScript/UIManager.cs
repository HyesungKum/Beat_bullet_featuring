using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class UIManager : MonoBehaviour
{
    private AudioManager audio;
    float[] spectrum = new float[64];

    RectTransform[] Bars = new RectTransform[7];

    Transform popPosition;

    StringBuilder MSD;

    GameObject prefab;

    float timer = 0f;
    bool started = false;

    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        MSD = new StringBuilder();
        popPosition = GameObject.Find("PopZone").GetComponent<Transform>();

        prefab = Resources.Load<GameObject>("MeleeNote");
        //for (int i = 0; i < 7; i++)
        //{
        //    string name = "Bar" + i.ToString();
        //    Bars[i] = GameObject.Find(name).GetComponent<RectTransform>();
        //}

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
        Recording();
    }

    private void FFTBar()
    {
        audio.GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 0; i < Bars.Length; i++)
        {
            Bars[i].sizeDelta = Vector2.Lerp(Bars[i].sizeDelta, new Vector2(20f, spectrum[i * 10 + 1] * ((i * 2 + 1) * 700)), 0.1f);
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
                if (Bars[2].sizeDelta.y > 40f)//average
                {
                    Debug.Log("boom");
                    MSD.Append("M,");
                    ObjectPool.Instance.PopObject(popPosition.position, prefab);
                }
                else
                {
                    Debug.Log("sheeee");
                    MSD.Append("N,");
                }
            }
        }
        else if (!audio.GetComponent<AudioSource>().isPlaying && started)
        {
            Debug.Log("saving");
            started = false;
            string path = @"C:\Users\User\Desktop\Unity_project\Beat_bullet_featuring\Assets\Resources\MSD.csv";
            File.WriteAllText(path, MSD.ToString());

        }

        Debug.Log(audio.GetComponent<AudioSource>().isPlaying);
    }
}

//public class CSVWriter
//{
//    public static void Write(List<List<object>> dataList, string fileName)
//    {
//        int dataCount = dataList.Count;
//        List<List<string>> output = new List<List<string>>();
//
//        for (int i = 0; i < dataCount; i++)
//        {
//            List<string> lines = new List<string>();
//            for (int j = 0; j < dataList[i].Count; j++)
//            {
//                string strData = dataList[i][j].ToString();
//                lines.Add(strData);
//            }
//            output.Add(lines);
//        }
//
//        int length = output[0].Count;
//        string delimiter = ",";
//        StringBuilder sb = new StringBuilder();
//
//        for (int index = 0; index < length; index++)
//            sb.AppendLine(string.Join(delimiter, output[index]));
//
//        string filePath = GetPath();
//        if (!Directory.Exists(filePath))
//        {
//            Directory.CreateDirectory(filePath);
//        }
//        fileName += ".csv";
//
//
//        StreamWriter outStream = System.IO.File.CreateText(filePath + fileName);
//        outStream.WriteLine(sb);
//        outStream.Close();
//    }
//
//    private static string GetPath()
//    {
//#if UNITY_EDITOR
//        return Application.dataPath + "/Resources/CSV/";
//#elif UNITY_ANDROID
//        return Application.persistentDataPath + "/Resources/CSV/";
//#elif UNITY_IPHONE
//        return Application.persistentDataPath +"/Resources/CSV/";
//#else
//        return Application.dataPath +"/Resources/CSV/";
//#endif
//    }
//}