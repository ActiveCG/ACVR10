using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleMov : MonoBehaviour
{
    public GameObject mapNav;

    public enum _axis { x, y, z };
    public _axis axis;

    public float multiplier;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == mapNav)
        {
            switch (axis)
            {
                case _axis.x:
                    mapNav.GetComponent<Mimic>().scalarX = multiplier;
                    break;
                case _axis.y:
                    mapNav.GetComponent<Mimic>().scalarY = multiplier;
                    break;
                case _axis.z:
                    mapNav.GetComponent<Mimic>().scalarZ = multiplier;
                    break;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == mapNav)
        {
            print("Test");
            mapNav.GetComponent<Mimic>().offset += mapNav.transform.position - (GameObject.FindGameObjectWithTag("Player").transform.position + mapNav.GetComponent<Mimic>().offset);
            mapNav.GetComponent<Mimic>().scalarX = 1;
            mapNav.GetComponent<Mimic>().scalarY = 1;
            mapNav.GetComponent<Mimic>().scalarZ = 1;
        }
    }
}