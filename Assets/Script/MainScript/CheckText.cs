using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckText : MonoBehaviour
{
    [SerializeField] AnimationCurve textPositionCurve = new AnimationCurve(new Keyframe[] {new Keyframe(0f, 0f), new Keyframe(0.5f, 100f)});
    [SerializeField] AnimationCurve textAlphaCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 1f), new Keyframe(0.5f, 0f) });


    //onenable √ ±‚»≠

    Vector3 initPosition = Vector3.zero;
    Color initColor = Color.white;

    private void Awake()
    {
        initPosition = this.transform.position;
        initColor = this.transform.GetComponent<TextMeshProUGUI>().color;
    }

    private void OnEnable()
    {
        StartCoroutine(TextMove());
        StartCoroutine(TextColor());
        StartCoroutine(TextPush());
    }

    private void OnDisable()
    {
        this.transform.position = Vector3.zero;
        this.transform.GetComponent<TextMeshProUGUI>().color = initColor;
    }

    IEnumerator TextMove()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;

            Vector3 newPosition = new Vector3(this.transform.position.x, this.transform.position.y + textPositionCurve.Evaluate(timer), initPosition.z);
            this.transform.position = newPosition;

            if (timer >= 1f) yield break;

            yield return null;
        }
    }
    IEnumerator TextColor()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;

            this.GetComponent<TextMeshProUGUI>().alpha = textAlphaCurve.Evaluate(timer);

            yield return null;
        }
    }
    IEnumerator TextPush()
    {
        yield return new WaitForSeconds(1f);
        if (this.gameObject.name == "BadText(Clone)")
        {
            ObjectPool.Instance.PushObject(this.gameObject, "checkBadText");
        }
        else if (this.gameObject.name == "GoodText(Clone)")
        {
            ObjectPool.Instance.PushObject(this.gameObject, "checkGoodText");
        }
        else if (this.gameObject.name == "ExellentText(Clone)")
        {
            ObjectPool.Instance.PushObject(this.gameObject, "checkExText");
        }
    }
}
