using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //variable value
    bool isPushed = false;

    // Update is called once per frame
    void Update()
    {
        InputControll();
    }

    private void InputControll()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPushed = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameManager.Instance.Pause)
            {
                GameManager.Instance.Pause = false;
            }
            else
            {
                GameManager.Instance.Pause = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isPushed)
        {
            if (collision.CompareTag("Note"))
            {
                collision.SendMessage("Check", SendMessageOptions.DontRequireReceiver);
            }
            isPushed = false;
        }
    }

}
