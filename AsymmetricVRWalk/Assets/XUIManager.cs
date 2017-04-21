using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class XUIManager : MonoBehaviour {

    public GameObject HMD;
    public GameObject NonHMD;

    public void HMDButtonPress(int rank)
    {
        TestManager.instance.HMD = rank.ToString();
        TestManager.instance.safe1 = true;
        HMD.SetActive(false);
    }
    public void NavButtonPress(int rank)
    {
        TestManager.instance.nonHMD = rank.ToString();
        TestManager.instance.safe2 = true;
        NonHMD.SetActive(false);
    }
    
    void Update()
    {
        if((HMD.activeInHierarchy == true || NonHMD.activeInHierarchy == true) && Input.GetKeyDown("space"))
        {
            if (TestManager.instance.HMD == null)
            {
                TestManager.instance.HMD = "Null";
            }
            if(TestManager.instance.nonHMD == null)
            {
                TestManager.instance.nonHMD = "Null";
            }
            
            TestManager.instance.safe1 = true;
            TestManager.instance.safe2 = true;
            HMD.SetActive(false);
            NonHMD.SetActive(false);            
        }
    }
}
