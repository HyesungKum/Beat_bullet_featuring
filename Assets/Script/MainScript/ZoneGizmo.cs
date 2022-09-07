using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGizmo : MonoBehaviour
{
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        switch (this.transform.name)
        {
            case "ExellentZone":
                Gizmos.color = new Color(0f, 255f, 0f, 0.5f);
                break;
            case "GoodZone":
                Gizmos.color = new Color(255f, 255f, 0f, 0.5f);
                break;
            case "BadZone":
                Gizmos.color = new Color(255f, 0f, 50f, 0.5f);
                break;
        }
        Gizmos.DrawCube(this.transform.position, this.transform.lossyScale);
    }
#endif
}
