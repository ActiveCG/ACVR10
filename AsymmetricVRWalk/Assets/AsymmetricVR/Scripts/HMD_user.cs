using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HMD_user : MonoBehaviour {

	//[HideInInspector]
	public GameObject currentCell;
	private Text timerText;
	private bool hunting;

	private float startTime;

	void Start(){
		//currentCell = GameObject.Find ("Cell1");
		timerText = GameObject.FindGameObjectWithTag ("timerText").GetComponent<Text> ();
		timerText.text = "00:00";
		hunting = false;
		startTime = 0f;
	}

	public delegate void GameAction(bool state);
	public event GameAction OnTrespassed;

	public void trespassed(bool state){
		if (OnTrespassed != null) {
			OnTrespassed (state);
		}
	}


	public void huntingChanged(bool state){
		hunting = state;
		startTime = Time.time;
	}

	void Update(){
		if (hunting == true) {
			int currentTime = (int) (Time.time - startTime);
			int min = currentTime / 60;
			int sec = currentTime % 60;
			timerText.text = min + ":" + sec;
		}
	}
}
