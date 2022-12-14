using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //components
    Transform playerNode;
    Transform playerLeft;
    Transform playerRight;

    Vector3 playerInitNode = Vector3.zero;

    Vector3 playerNodeScale = Vector3.zero;
    Vector3 playerRightScale = Vector3.zero;
    Vector3 playerLeftScale = Vector3.zero;

    [SerializeField] AnimationCurve nodePulseCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 500f) });
    [SerializeField] AnimationCurve RLPulseCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 500f) });
    [SerializeField] AnimationCurve RDodgeCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 500f) });
    [SerializeField] AnimationCurve LDodgeCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 500f) });
    [SerializeField] AnimationCurve DodgeCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 500f) });

    float timer = 0f;

    //variable value
    bool RisPushed = false;
    bool LisPushed = false;
    bool DisPushed = false;

    private void Awake()
    {
        playerNode = GameObject.Find("PlayerNode").GetComponent<Transform>();
        playerLeft = GameObject.Find("PlayerLeft").GetComponent<Transform>();
        playerRight = GameObject.Find("PlayerRight").GetComponent<Transform>();
    }

    void Start()
    {
        playerNodeScale = playerNode.localScale;
        playerRightScale = playerRight.localScale;
        playerLeftScale = playerLeft.localScale;

        playerInitNode = playerNode.position;
    }

    void Update()
    {
        if (GameManager.Instance.SelectMusicSquence) return;
        InputControll();

        timer += Time.deltaTime;
        if (timer >= (60f / 142f))
        {
            timer = 0f;
        }

        playerNode.transform.localScale = playerNodeScale + Vector3.one * nodePulseCurve.Evaluate(timer);

        //main key input
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RisPushed = true;
            StartCoroutine(UsedRight());
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            RisPushed = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LisPushed = true;
            StartCoroutine(UsedLeft());
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            LisPushed = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            DisPushed = true;
            StartCoroutine(UsedLeftDodge());
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            DisPushed = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DisPushed = true;
            StartCoroutine(UsedDodge());
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            DisPushed = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DisPushed = true;
            StartCoroutine(UsedRightDodge());
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            DisPushed = false;
        }
    }
    #region NodeMotion
    IEnumerator UsedRight()
    {
        float Timer = 0f;

        while (true)
        {
            Timer += Time.deltaTime;

            if (Timer >= (60f / 142f) / 10f) yield break;

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

            if (Timer >= (60f / 142f) / 10f) yield break;

            playerLeft.transform.localScale = playerLeftScale + Vector3.one * RLPulseCurve.Evaluate(Timer);
            yield return null;
        }
    }
    IEnumerator UsedRightDodge()
    {
        float Timer = 0f;

        while (true)
        {
            Timer += Time.deltaTime;

            if (Timer >= (60f / 142f) / 10f) yield break;

            playerNode.position = new Vector3( playerInitNode.x + RDodgeCurve.Evaluate(Timer), playerInitNode.y, 0f );
            yield return null;
        }
    }
    IEnumerator UsedLeftDodge()
    {
        float Timer = 0f;

        while (true)
        {
            Timer += Time.deltaTime;

            if (Timer >= (60f / 142f) / 10f) yield break;

            playerNode.position = new Vector3( playerInitNode.x - RDodgeCurve.Evaluate(Timer), playerInitNode.y, 0f );
            yield return null;
        }
    }
    IEnumerator UsedDodge()
    {
        float Timer = 0f;

        while (true)
        {
            Timer += Time.deltaTime;

            if (Timer >= (60f / 142f) / 10f) yield break;

            playerNode.position = new Vector3(playerInitNode.x, playerInitNode.y - RDodgeCurve.Evaluate(Timer), 0f);
            yield return null;
        }
    }
    #endregion
    private void InputControll()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (GameManager.Instance.PopStart)
            {
                GameManager.Instance.PopStart = false;
            }
            else
            {
                GameManager.Instance.PopStart = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameManager.Instance.Pause)
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = Time.timeScale;
                GameManager.Instance.Pause = false;
            }
            else
            {
                Time.timeScale = 0f;
                Time.fixedDeltaTime = Time.timeScale;
                GameManager.Instance.Pause = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (RisPushed)
        {
            if (collision.CompareTag("RNote"))
            {
                collision.SendMessage("Check", SendMessageOptions.DontRequireReceiver);
            }
            RisPushed = false;
        }
        if (LisPushed)
        {
            if (collision.CompareTag("LNote"))
            {
                collision.SendMessage("Check", SendMessageOptions.DontRequireReceiver);
            }
            LisPushed = false;
        }
        if (DisPushed)
        {
            if (collision.CompareTag("Dodge"))
            {
                collision.SendMessage("Check", SendMessageOptions.DontRequireReceiver);
            }
            DisPushed = false;
        }
    }

}
