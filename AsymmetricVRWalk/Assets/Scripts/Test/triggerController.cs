using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerController : MonoBehaviour
{

    public GameObject trigF;
    public GameObject trigB;

    private bool trigger = false;

    // This implementation is counter intuitive as it would only require one collider, but for further implementation two make sense
    public void SwitchTrig()
    {
        trigF.SetActive(trigger);
        trigger = !trigger;
        trigB.SetActive(trigger);
    }
}
