using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new Vector3(0, 3f, -3f);

    private void LateUpdate()
    {
        transform.position = Target.position + Offset;
        transform.LookAt(Target);
    }
}
