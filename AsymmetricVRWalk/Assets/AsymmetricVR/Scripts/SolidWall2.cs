using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SolidWall2 : MonoBehaviour {

	private bool crossedWall;
	private bool inWall;
	private bool checkCrossBack;
	private GameObject currentCorridor;
	private GameObject hmd_user;

	void Start () {
		crossedWall = false;
		inWall = false;
		checkCrossBack = false;
		currentCorridor = GameObject.Find ("Cell1/col1");
		hmd_user = transform.parent.gameObject;
	}

	void Update(){
		if (checkCrossBack == true) {
			BoxCollider[] corridorColliders = currentCorridor.GetComponents<BoxCollider> ();
			for (int c = 0; c < corridorColliders.Length; c++) {
				if(corridorColliders[c].bounds.Contains(transform.TransformPoint(GetComponent<BoxCollider>().center))){
					Transform cell = hmd_user.GetComponent<HMD_user>().currentCell.transform;
					foreach (Transform child in cell) {
						if (child != cell) {
							child.gameObject.SetActive (true);
						}
					}
					crossedWall = false;
					inWall = false;
					checkCrossBack = false;
					break;
				}
			}
		}
	}

	void OnTriggerExit(Collider collider){
		if (collider.tag == "CorridorBox" && inWall == true && crossedWall == false && collider.gameObject == currentCorridor) {

			BoxCollider[] corridorColliders = currentCorridor.GetComponents<BoxCollider> ();

			Collider[] col = null; //corridor colliders to render

			for (int c = 0; c < corridorColliders.Length; c++) {
				Collider[] overlappedColls = 
					Physics.OverlapBox (currentCorridor.transform.TransformPoint(corridorColliders [c].center), 
						corridorColliders [c].size/2f);
				if (col == null) {
					col = new Collider[overlappedColls.Length];
					int i = 0;
					foreach (Collider obj in overlappedColls)
						col[i++] = obj;
				} else {
					Collider[] temp = new Collider[col.Length];
					temp = col;
					col = new Collider[temp.Length + overlappedColls.Length];
					int i = 0;
					foreach (Collider obj in temp)
						col[i++] = obj; 
					foreach (Collider obj in overlappedColls)
						col [i++] = obj;
				}
			}

			Transform cell = hmd_user.GetComponent<HMD_user>().currentCell.transform;
			foreach (Transform child in cell)
			{
				if (child != cell){
					child.gameObject.SetActive (false);
				}
			}
			for (int c = 0; c < col.Length; c++) {
				col [c].gameObject.SetActive (true);
			}

			crossedWall = true;
			checkCrossBack = false;
		}

		if (collider.tag == "Wall") {
			//check whether player is back in corridor or more out
			BoxCollider[] corridorColliders = currentCorridor.GetComponents<BoxCollider> ();
			for (int c = 0; c < corridorColliders.Length; c++) {
				if(corridorColliders[c].bounds.Contains(transform.position)){
					inWall = false;
				}
			}
		}
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
