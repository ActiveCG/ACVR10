using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    public static TestManager instance = null;

    StreamWriter file;

    public GameObject likertUI;

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

    [HideInInspector]
    public bool fade;

    private string Crash; 

    private float timer2;


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
            writeTimer = true;
            likertUI.SetActive(true);
            Crash = " The system broke";
        }
        if (startTimer == true)
        {
            globalTimer += Time.deltaTime;
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
            file.WriteLine("Time: " + time + "," + " HMD: " + HMD + "," + " nonHMD: " + nonHMD + "," + Crash);
            file.Close();

            safe1 = false;
            safe2 = false;

            winning = true;
            fade = true;
        }
        if (fade == true)
        {
            SteamVR_Fade.Start(Color.black, 5);
            timer2 += Time.deltaTime;
            if (timer2 > 5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                fade = false;
            }
        }
    }
    public void EndGame(GameObject HMD, GameObject Map)
    {
        likertUI.SetActive(false);
        HMD.SetActive(false);
        Map.SetActive(false);
        startTimer = false;
        fade = true;
    }
}
