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

    public float waitTime;

	void Start(){
		map.SetActive (false);
	}

    void Update()
    {
        if (startTimer == true)
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                calibrationCell.SetActive(false);
                startingCell.SetActive(true);
                map.SetActive(true);
                SteamVR_Fade.Start(Color.clear, 1);
                timer = 0;
                startTimer = false;
            }
        }
    }

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
