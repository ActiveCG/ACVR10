using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerController : MonoBehaviour
{

    public GameObject trigF;
    public GameObject trigB;

    public float transitionLengthX;
    public float transitionLengthY;
    public float transitionLengthZ;

    public float speed;

    private bool trigger = false;

    // This implementation is counter intuitive as it would only require one collider, but for further implementation two make sense
    public void SwitchTrig()
    {
        print("Debug");
        trigF.SetActive(trigger);
        trigger = !trigger;
        trigB.SetActive(trigger);

        transitionLengthX *= -1;
        transitionLengthY *= -1;
        transitionLengthZ *= -1;
    }
}
