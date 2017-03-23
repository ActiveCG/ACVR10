using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour {

	public GameObject hidePart;
	public GameObject showPart;

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Player") {
			showPart.SetActive (true);
			hidePart.SetActive (false);
		}
	}
}
