using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class calibrationTrigger : MonoBehaviour
{

    private float timer;
    private bool startTimer;

    public GameObject calibrationCell;
    public GameObject startingCell;
    public GameObject map;
    public GameObject ui;
    public GameObject mapCell0;

    public float waitTime;

    // Use this for initialization
    void Update()
    {
        if (startTimer == true)
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                calibrationCell.SetActive(false);
                mapCell0.SetActive(false);
                startingCell.SetActive(true);
                map.SetActive(true);
                SteamVR_Fade.Start(Color.clear, 1);
                TestManager.instance.startTimer = true;
                timer = 0;
                startTimer = false;
                GameObject mimic = GameObject.FindGameObjectWithTag("Mimic");
                mimic.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + map.transform.position;
                ui.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            SteamVR_Fade.Start(Color.black, waitTime);
            startTimer = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            startTimer = false;
            SteamVR_Fade.Start(Color.clear, 1);
            timer = 0;
        }
    }
}
