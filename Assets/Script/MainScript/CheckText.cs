using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckText : MonoBehaviour
{
    [SerializeField] AnimationCurve checkTextCurve = new AnimationCurve(new Keyframe[] {new Keyframe(0f, 0f), new Keyframe(1f, 100f)});
    
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
