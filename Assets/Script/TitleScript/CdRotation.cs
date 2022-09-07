using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CdRotation : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 0.1f;

    void Update()
    {
        transform.Rotate(0f, 0f, 0.2f);
    }
}
