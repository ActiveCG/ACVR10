using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class youWin : MonoBehaviour {


    public GameObject mapYouWin;
    public GameObject hmdYouWin;
    public GameObject mapKalibrer;
    public GameObject likertUI;

    bool fade;
    bool startTimer;

    float timer;

    void Start()
    {
        fade = false;
        startTimer = false;
        timer = 0;
    }

	// Update is called once per frame
	void Update ()
    {
		if(startTimer == true)
        {
            timer += Time.deltaTime;
            if (timer > 30 && TestManager.instance.winning == true)
            {
                TestManager.instance.EndGame(mapYouWin, hmdYouWin);
                timer = 0;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            likertUI.SetActive(true);
            hmdYouWin.SetActive(true);
            mapYouWin.SetActive(true);

            TestManager.instance.writeTimer = true;
            startTimer = true;
        }
    }
}
