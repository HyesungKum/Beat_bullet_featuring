using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNode : MonoBehaviour
{
    // Start is called before the first frame update
    Transform playerNode;
    Transform playerLeft;
    Transform playerRight;

    [SerializeField] AnimationCurve nodePulseCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 500f)});

    Vector3 playerNodeScale;

    private void Awake()
    {
        playerNode = gameObject.GetComponent<Transform>();
        playerLeft = GameObject.Find("PlayerLeft").GetComponent<Transform>();
        playerRight = GameObject.Find("PlayerRight").GetComponent<Transform>();


    }
    void Start()
    {
        playerNodeScale = playerNode.lossyScale;
        StartCoroutine(Pulse());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Pulse()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= 1f) timer = 0f;
            playerNode.transform.localScale = Vector3.one * nodePulseCurve.Evaluate(timer);

            yield return null;
        }
    }
}
