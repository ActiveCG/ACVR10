using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpMov : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    void OnTriggerEnter(Collider col)
    {
        startPos = col.transform.position;
    }

    void OnTriggerExit(Collider col)
    {
        endPos = col.transform.position;
    }
}
