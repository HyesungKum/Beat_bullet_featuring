using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    GameObject loadingPopZone;

    GameObject loadingBulletPrefab;

    GameObject[] textList;

    AsyncOperation ao;

    //constant value
    [SerializeField] float textDelay = 1f;

    private float loadingValue;
    private float loadingTimer;

    private float progressTimer;
    private float prevProgress;

    private float fallCount = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        loadingPopZone = GameObject.Find("LoadingBulletPopZone");
        loadingBulletPrefab = Resources.Load<GameObject>("LoadingBullet");

        textList = GameObject.FindGameObjectsWithTag("LoadingText");

        loadingValue = 0f;
        loadingTimer = 0f;

        prevProgress = 0f;

    }

    private void Start()
    {
        for (int i = 0; i < textList.Length; i++)
        {
            textList[i].SetActive(false);
        }
        StartCoroutine(TextOut());
        StartCoroutine(LoadMainScene());
    }

    IEnumerator TextOut()
    {
        int index = 0;

        while (true)
        {
            textList[index].SetActive(true);

            if (index != 0)
            {
                textList[index - 1].SetActive(false);
            }

            index++;

            if (index >= textList.Length)
            {
                textList[index].SetActive(false);
                index = 0;
            }

            yield return new WaitForSeconds(textDelay);
        }
    }

    IEnumerator LoadMainScene()
    {
        yield return null;
        ao = SceneManager.LoadSceneAsync("MainScene",LoadSceneMode.Single);
        ao.allowSceneActivation = false; //do not change scene after loading done

        while (!ao.isDone)
        {
            //until 0.9
            loadingTimer += Time.deltaTime/100f;
            loadingValue = Mathf.Lerp(loadingValue, ao.progress, loadingTimer);

            //0.9 to 1
            if (Mathf.Approximately(loadingValue, 0.9f))
            {
                StartCoroutine(ProgressBar());
                yield break;
            }

            if (loadingValue * 10f - (float)fallCount >= 0.5f)//bullet fall connect loading
            {
                fallCount += 0.5f;
                Vector3 DropPosition = loadingPopZone.transform.position + new Vector3(Random.Range(-10f, 10f), 0f, 0f);
                GameObject loadingBullet = Instantiate(loadingBulletPrefab, loadingPopZone.transform.position, Quaternion.Euler(new Vector3 (0f, 0f, Random.Range(0f, 360f))));
            }

            yield return null;
        }
    }

    IEnumerator ProgressBar()
    {
        while (loadingValue < 1f)
        {
            progressTimer += Time.deltaTime / 10f;
            loadingValue = Mathf.Lerp(loadingValue, 1f, loadingTimer);

            if (loadingValue > 0.997f)
            {
                loadingValue = 1f;
            }

            if (Mathf.Approximately(loadingValue, 1f))//per 1sec
            {
                ao.allowSceneActivation = true;
                yield break;
            }

            yield return null;
        }
    }
}
