using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private float noteSpeed = 30f;

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
        NoteState = (int)State.None;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.right * noteSpeed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ExellentZone"))
        {
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
            ObjectPool.Instance.PushObject(gameObject);
        }
    }

    private void Check()
    {
        if (this.NoteState == (int)State.Bad)
        {
            GameManager.Instance.Combo = 0;
        }
        else
        {
            GameManager.Instance.Combo++;
        }
        ObjectPool.Instance.PushObject(gameObject);
    }
}
