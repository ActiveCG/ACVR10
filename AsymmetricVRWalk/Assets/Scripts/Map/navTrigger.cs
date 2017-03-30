using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navTrigger : MonoBehaviour
{
    public float x;
    public float y;
    public float z;

    private bool yee_ol_switcheroo = false;
    private static bool inTrigger = false;

    void Update()
    {
        print(inTrigger);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mimic")
        {
            if (inTrigger == false)
            {
                if (yee_ol_switcheroo == false)
                {
                    other.GetComponent<Navigation>().UpdateOffset(x, y, z);
                }
                else if (yee_ol_switcheroo == true)
                {
                    other.GetComponent<Navigation>().UpdateOffset(-x, -y, -z);
                }
                inTrigger = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Mimic")
        {
            yee_ol_switcheroo = !yee_ol_switcheroo;
            inTrigger = false;
        }
    }
}
