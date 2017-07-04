using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class youWin : MonoBehaviour {

	//text to show as reward
    public GameObject mapYouWin;
    public GameObject hmdYouWin;

    bool fade;
    bool startTimer;

    float timer2;//fade timer
    float timer;//reward showing timer

    void Start()
    {
        fade = false;
        startTimer = false;
        timer = 0;
		timer2 = 0;
    }

	// Update is called once per frame
	void Update ()
    {
		//timer while showing winning rewards
		if(startTimer == true)
        {
            timer += Time.deltaTime;
            if (timer > 18)
            {
                //hmdYouWin.SetActive(false);
                //mapYouWin.SetActive(false);
                timer = 0;
                startTimer = false;
                fade = true;
            }
        }
		//fade out the scene
        if(fade == true)
        {
            SteamVR_Fade.Start(Color.black, 5);
            timer2 += Time.deltaTime;
            if (timer2 > 5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                fade = false;
				timer2 = 0;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            hmdYouWin.SetActive(true);
            mapYouWin.SetActive(true);

            startTimer = true;
			GameObject.FindObjectOfType<HMD_user> ().huntingChanged (false);
        }
    }
}
