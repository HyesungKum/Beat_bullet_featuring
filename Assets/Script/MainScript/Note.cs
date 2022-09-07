using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Note : MonoBehaviour
{
    //components
    Transform mainCamTransform;

    GameObject exellentPrefab;
    GameObject goodPrefab;
    GameObject badPrefab;

    //constant value
    [SerializeField] private float noteSpeed = 30f;
    private Vector2 initCamTransform;

    //variable value

    float time = 0f;

    public int NoteState { get; set; }

    private enum State
    {
        None,
        Bad,
        Good,
        Exellent,
        Max
    }
    private void Awake()
    {
        //component
        exellentPrefab = Resources.Load<GameObject>("ExellentText");
        goodPrefab = Resources.Load<GameObject>("GoodText");
        badPrefab = Resources.Load<GameObject>("BadText");

        NoteState = (int)State.None;
        mainCamTransform = GameObject.Find("MainCamera").GetComponent<Transform>();
        initCamTransform = mainCamTransform.position;
    }

    void Update()
    {
        transform.Translate(-transform.right * noteSpeed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ExellentZone"))
        {
            //Debug.Log(Time.time - time); need to check changed note bar
            //music must played together first note alive
            NoteState = (int)State.Exellent;
        }
        else if (collision.CompareTag("GoodZone"))
        {
            NoteState = (int)State.Good;
        }
        else if (collision.CompareTag("BadZone"))
        {
            NoteState = (int)State.Bad;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GoodZone"))
        {
            NoteState = (int)State.None;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            ObjectPool.Instance.PushObject(gameObject, "meleeNote");
        }
    }

    private void Check()
    {
        if (this.NoteState == (int)State.Bad)
        {
            //StartCoroutine(camshaking());
            Vector3 textPosition = Camera.main.WorldToScreenPoint(this.transform.position);

            ObjectPool.Instance.PopObject(textPosition, badPrefab, "checkBadText");

            GameManager.Instance.Combo = 0;
        }
        else if (this.NoteState == (int)State.Good)
        {
            Vector3 textPosition = Camera.main.WorldToScreenPoint(this.transform.position);

            ObjectPool.Instance.PopObject(textPosition, goodPrefab, "checkGoodText");

            GameManager.Instance.Combo++;
        }
        else if (this.NoteState == (int)State.Exellent)
        {
            Vector3 textPosition = Camera.main.WorldToScreenPoint(this.transform.position);

            ObjectPool.Instance.PopObject(textPosition, exellentPrefab, "checkExText");

            GameManager.Instance.Combo++;
        }
        ObjectPool.Instance.PushObject(gameObject, "meleeNote");
    }

    //IEnumerator camshaking()
    //{
    //    bool flag = true;
    //}
}
