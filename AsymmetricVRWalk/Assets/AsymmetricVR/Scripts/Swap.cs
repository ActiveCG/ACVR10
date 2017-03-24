using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour {

	public GameObject hidePart;
	public GameObject showPart;

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.GetComponent<HMD_user> ()/*collider.tag == "Player"*/) {
			showPart.SetActive (true);
			hidePart.SetActive (false);

			collider.gameObject.GetComponent<HMD_user> ().currentCell = showPart;
		}
	}
}
