using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManger : MonoBehaviour
{
    Transform playerNode;
    Transform playerLeft;
    Transform playerRight;

    Vector3 playerNodeScale = Vector3.zero;
    Vector3 playerRightScale = Vector3.zero;
    Vector3 playerLeftScale = Vector3.zero;

    [SerializeField] AnimationCurve nodePulseCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 500f) });
    [SerializeField] AnimationCurve RLPulseCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 500f) });

    //
    AudioSource audioSource;
    [SerializeField] AudioClip metronom;

    float timer = 0f;

    bool start;

    private void Awake()
    {
        playerNode = GameObject.Find("PlayerNode").GetComponent<Transform>();
        playerLeft = GameObject.Find("PlayerLeft").GetComponent<Transform>();
        playerRight = GameObject.Find("PlayerRight").GetComponent<Transform>();

        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        playerNodeScale = playerNode.localScale;
        playerRightScale = playerRight.localScale;
        playerLeftScale = playerLeft.localScale;

        start = false;
    }

    private void OnEnable()
    {
        StartCoroutine(Delay(4f));
    }
   
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            
            timer += Time.deltaTime;
            if (timer >= (60f / 142f))
            {
                timer = 0f;
                audioSource.clip = metronom;
                audioSource.PlayOneShot(metronom);
            }


            playerNode.transform.localScale = playerNodeScale + Vector3.one * nodePulseCurve.Evaluate(timer);

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartCoroutine(UsedRight());
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartCoroutine(UsedLeft());
            }
        }
    }
    private void OnDisable()
    {
        start = false;
    }
    IEnumerator UsedRight()
    {
        float Timer = 0f;

        while(true)
        {
            Timer += Time.deltaTime;

            if (Timer >= (60f / 142f) / 2f) yield break;

            playerRight.transform.localScale = playerRightScale + Vector3.one * RLPulseCurve.Evaluate(Timer);
            yield return null;
        }
    }
    IEnumerator UsedLeft()
    {
        float Timer = 0f;

        while (true)
        {
            Timer += Time.deltaTime;

            if (Timer >= (60f / 142f) / 2f) yield break;

            playerLeft.transform.localScale = playerLeftScale + Vector3.one * RLPulseCurve.Evaluate(Timer);
            yield return null;
        }
    }

    IEnumerator Delay(float delayTime)
    {
        float Timer = 0f;

        while (true)
        {
            Timer += Time.deltaTime;

            if (Timer > delayTime)
            {
                start = true;
                yield break;
            }

            yield return null;
        }
    }
}
