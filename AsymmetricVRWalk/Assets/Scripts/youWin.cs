using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class youWin : MonoBehaviour {


    public GameObject mapYouWin;
    public GameObject hmdYouWin;
    public GameObject mapKalibrer;

    bool fade;
    bool startTimer;

    float timer2;
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
                hmdYouWin.SetActive(false);
                mapYouWin.SetActive(false);
                timer = 0;
                startTimer = false;
                fade = true;
            }
        }
        if(fade == true)
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            hmdYouWin.SetActive(true);
            mapYouWin.SetActive(true);

            TestManager.instance.writeTimer = true;
            startTimer = true;
        }
    }
}
