using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidWall2 : MonoBehaviour {

	private bool crossedWall;
	private bool inWall;
	private bool checkCrossBack;
	private GameObject currentCorridor;

	void Start () {
		crossedWall = false;
		inWall = false;
		checkCrossBack = false;
		currentCorridor = GameObject.Find ("Cell1/col1");
	}

	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Wall") {
			inWall = true;
		}

		if (collider.tag == "CorridorBox"){
			if (crossedWall == true && collider.gameObject == currentCorridor) {
				checkCrossBack = true;
			} else if (crossedWall == false && inWall == false) {
				currentCorridor = collider.gameObject;
			}
		}
	}
}
