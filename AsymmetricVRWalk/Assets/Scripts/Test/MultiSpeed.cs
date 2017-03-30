using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSpeed : MonoBehaviour
{
    [HideInInspector]
    public float transitionLengthX;
    [HideInInspector]
    public float transitionLengthY;
    [HideInInspector]
    public float transitionLengthZ;
    [HideInInspector]
    public float speed;

    private bool toggle;

    void Start()
    {
        transitionLengthX = GetComponentInParent<triggerController>().transitionLengthX;
        transitionLengthY = GetComponentInParent<triggerController>().transitionLengthY;
        transitionLengthZ = GetComponentInParent<triggerController>().transitionLengthZ;
    }

    void OnTriggerEnter(Collider col)
    {
            GetComponentInParent<triggerController>().SwitchTrig();
    }
}
