using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static TestManager instance = null;

    StreamWriter file;

    [HideInInspector]
    public string HMD;
    [HideInInspector]
    public string nonHMD;

    [HideInInspector]
    public string time;

    [HideInInspector]
    public float globalTimer;
    [HideInInspector]
    public bool startTimer;

    [HideInInspector]
    public bool writeTimer;
    [HideInInspector]
    public bool manualWriteTimer;

    [HideInInspector]
    public bool safe1;
    [HideInInspector]
    public bool safe2;

    [HideInInspector]
    public bool winning;

    public string fileDest;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        startTimer = false;

        writeTimer = false;
        manualWriteTimer = false;

        safe1 = false;
        safe2 = false;

        winning = false;

        file = new StreamWriter(@fileDest, true);
    }

    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            manualWriteTimer = true;
        }
        if (startTimer == true)
        {
            globalTimer += Time.deltaTime;
        }
        if (manualWriteTimer == true)
        {
            file.WriteLine("Manual Time:" + globalTimer);
            file.Close();

            manualWriteTimer = false;
            startTimer = false;
            globalTimer = 0;
        }
        if(writeTimer == true)
        {
            time = globalTimer.ToString();

            startTimer = false;
            globalTimer = 0;
            writeTimer = false;        
        }
        if(safe1 == true && safe2 == true)
        {
            file.WriteLine("Time: " + time + "," + " HMD: " + HMD + "," + " nonHMD: " + nonHMD);
            file.Close();

            safe1 = false;
            safe2 = false;

            winning = true;
        }
    }
}
