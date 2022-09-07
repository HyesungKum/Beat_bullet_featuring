using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopZone : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(this.transform.position,Vector3.one);
    }
}
