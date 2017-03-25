using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidWallSide : SolidWall {

	private GameObject hmd_user;

	[HideInInspector]
	public GameObject grid;

	void Start () {
		crossedWall = false;
		inWall = false;
		checkCrossBack = false;
		wallsEntered = new List<BoxCollider> ();
		hmd_user = transform.parent.gameObject;
	}
	

	void Update () {
		if (checkCrossBack == true) {
			if (IsInCorridor (transform.TransformPoint (GetComponent<BoxCollider> ().center)) == true) {
				ShowCell (hmd_user.GetComponent<HMD_user>().currentCell.transform);
				ShowGrid (grid, false);

				crossedWall = false;
				//inWall = false;
				checkCrossBack = false;
			}
		}
		//check whether still in wall
		wallsEntered = CheckStayingInWall (wallsEntered);
		if (wallsEntered.Count == 0)
			inWall = false;
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Wall") {
			inWall = true;
			wallsEntered.Add (collider as BoxCollider);
		}

		if (collider.tag == "CorridorBox"){
			//check whether coming back in corridor
			if (crossedWall == true && collider.gameObject == currentCorridor) {
				checkCrossBack = true;
			} 
			//entering next corridor
			else if (crossedWall == false && inWall == false) {
				//currentCorridor = collider.gameObject;
			}
		}
	}

	void OnTriggerExit(Collider collider){
		//exiting the corridor
		if (collider.tag == "CorridorBox" && inWall == true
			&& crossedWall == false && collider.gameObject == currentCorridor) {
			ShowOnlyOneCorridor (hmd_user.GetComponent<HMD_user>().currentCell.transform);
			ShowGrid (grid, true);

			crossedWall = true;
			checkCrossBack = false;
		}
		//exiting wall?
		if (collider.tag == "Wall") {
			//if(IsInCorridor(transform.TransformPoint(GetComponent<BoxCollider>().center)) == true){
				inWall = false;
				wallsEntered.Remove (collider as BoxCollider);
			//}
		}
	}
}
