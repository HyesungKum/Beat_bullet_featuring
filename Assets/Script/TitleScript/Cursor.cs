using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] AnimationCurve moveCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, -500f), new Keyframe(2f, 2000f) });

    private Vector2 initPosition;

    // Update is called once per frame
    void Update()
    {
        initPosition = transform.position;
    }

    void Action(float time)
    {
        StartCoroutine(DynamicMove(time));
    }

    IEnumerator DynamicMove(float time)
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;

            this.transform.position = new Vector2(this.transform.position.x + moveCurve.Evaluate(timer), this.transform.position.y);

            if (timer >= 3f)
            {
                yield break;
            }

            yield return null;
        }
    }
}

