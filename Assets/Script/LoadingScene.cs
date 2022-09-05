using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    Slider loadingSlider;
    AsyncOperation ao;

    private float loadingValue;
    private float loadingTimer;
    private float progressTimer;

    private float prevProgress;

    // Start is called before the first frame update
    void Start()
    {
        loadingSlider = GameObject.Find("LoadingSlider").GetComponent<Slider>();

        if (loadingSlider == null) Debug.Log("Hi");

        loadingValue = 0f;
        loadingTimer = 0f;

        prevProgress = 0f;

        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        yield return null;
        ao = SceneManager.LoadSceneAsync("MainScene",LoadSceneMode.Single);
        ao.allowSceneActivation = false; //do not change scene after loading done

        while (!ao.isDone)
        {
            //until 0.9
            loadingTimer += Time.deltaTime/10f;

            loadingSlider.value = Mathf.Lerp(loadingSlider.value, ao.progress, loadingTimer);

            if (loadingSlider.value >= ao.progress)//per 1sec
            {
                loadingTimer = 0f;
            }

            //0.9 to 1
            if (ao.progress >= 0.9f)
            {
                StartCoroutine("ProgressBar");
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator ProgressBar()
    {
        while (loadingSlider.value < 1f)
        {
            progressTimer += Time.deltaTime / 10f;
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, 1f, loadingTimer);

            if (loadingSlider.value > 0.997f)
            {
                loadingSlider.value = 1f;
            }

            Debug.Log(loadingSlider.value);
            if (Mathf.Approximately(loadingSlider.value, 1f))//per 1sec
            {
                ao.allowSceneActivation = true;
                yield break;
            }

            yield return null;
        }
    }
}
