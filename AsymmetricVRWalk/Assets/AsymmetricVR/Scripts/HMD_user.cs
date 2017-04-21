using System.Collections;
using UnityEngine;

public class HMD_user : MonoBehaviour {

	//[HideInInspector]
	public GameObject currentCell;

	void Start(){
		//currentCell = GameObject.Find ("Cell1");
	}

	public delegate void TrespassAction(bool state);
	public event TrespassAction OnTrespassed;

	public void trespassed(bool state){
		if (OnTrespassed != null) {
			OnTrespassed (state);
		}
	}
}
