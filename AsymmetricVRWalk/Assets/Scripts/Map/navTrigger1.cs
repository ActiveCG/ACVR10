using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navTrigger1 : MonoBehaviour
{
    public float x;
    public float y;
    public float z;

    private bool yee_ol_switcheroo = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mimic")
        {
            if (yee_ol_switcheroo == false)
            {
                other.GetComponent<Navigation>().UpdateOffset(x, y, z);
            }
            else if (yee_ol_switcheroo == true)
            {
                other.GetComponent<Navigation>().UpdateOffset(-x, -y, -z);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Mimic")
        {
            GetComponentInParent<triggerController>().SwitchTrig();
        }
    }
}
