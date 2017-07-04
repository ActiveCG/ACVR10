using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidWallFront1 : SolidWall_1 {

	public SolidWallSide1 leftCollider;
	public SolidWallSide1 rightCollider;

	public Material worldSkybox;
	public Material outOfBoundsSkybox;

	private GameObject[] outOfBoundsObjects;

	void Start () {
		crossedWall = false;
		inWall = false;
		checkCrossBack = false;
		wallsEntered = new List<BoxCollider> ();
		//currentCorridor = GameObject.Find ("Cell1/corridor1");
		leftCollider.currentCorridor = currentCorridor;
		rightCollider.currentCorridor = currentCorridor;

		overlapCollider = GameObject.FindGameObjectWithTag ("OverlapCollider").GetComponent<BoxCollider> ();
		leftCollider.overlapCollider = overlapCollider;
		rightCollider.overlapCollider = overlapCollider;

		returnTrig = GameObject.FindGameObjectWithTag ("ReturnTrigger");
		leftCollider.returnTrig = returnTrig;
		rightCollider.returnTrig = returnTrig;
		returnTrig.SetActive(false);

		outOfBoundsObjects = GameObject.FindGameObjectsWithTag ("OutOfBounds");
		ShowOutOfBoundsObjects (false);

		panoramas = GameObject.FindGameObjectsWithTag ("Panorama");
		leftCollider.panoramas = panoramas;
		rightCollider.panoramas = panoramas;
	}


	void Update () {
		/*if (checkCrossBack == true) {
			if (IsInCorridor (transform.TransformPoint (GetComponent<BoxCollider> ().center)) == true) {
				ShowCell (GetComponent<HMD_user>().currentCell.transform);
				ShowOutOfBoundsObjects (false);
				//print ("front in");
				crossedWall = false;
				//inWall = false;
				checkCrossBack = false;
			}
		}*/

		//check whether entered return_trig
		if (crossedWall == true || leftCollider.crossedWall == true || rightCollider.crossedWall == true) {
			if (IsInReturnTrigger (GetComponent<BoxCollider>()) == true &&
				IsInReturnTrigger (rightCollider.GetComponent<BoxCollider>()) == true &&
				IsInReturnTrigger (leftCollider.GetComponent<BoxCollider>()) == true) {
				ShowCell (GetComponent<HMD_user>().currentCell.transform);
				ShowOutOfBoundsObjects (false);
				GetComponent<HMD_user> ().trespassed (false);

				crossedWall = false;
				leftCollider.crossedWall = false;
				rightCollider.crossedWall = false;
			}
		}

		//check whether still in wall
		wallsEntered = CheckStayingInWall (wallsEntered);
		if (wallsEntered.Count == 0)
			inWall = false;
		else
			inWall = true;
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Wall") {
			inWall = true;
			wallsEntered.Add (collider as BoxCollider);
		}

		if (collider.tag == "CorridorBox"){
			//check whether coming back in corridor
			/*if (crossedWall == true && collider.gameObject == currentCorridor) {
				checkCrossBack = true;
			} 
			//entering next corridor
			//if front not in wall and hasn't crossed wall and the left and right colliders have not crossed wall
			else*/ if (crossedWall == false && inWall == false
				&& leftCollider.crossedWall == false && rightCollider.crossedWall == false) {
				currentCorridor = collider.gameObject;
				leftCollider.currentCorridor = currentCorridor;
				rightCollider.currentCorridor = currentCorridor;
			}
		}
	}

	void OnTriggerExit(Collider collider){
		//exiting the corridor
		if (collider.tag == "CorridorBox" && inWall == true
			&& crossedWall == false && collider.gameObject == currentCorridor) {
			ShowOnlyOneCorridor (GetComponent<HMD_user>().currentCell.transform);
			ShowOutOfBoundsObjects (true);
			GetComponent<HMD_user> ().trespassed (true);

			crossedWall = true;
			//checkCrossBack = false;
			//print ("front out");
		}
		//exiting wall?
		if (collider.tag == "Wall") {
			//if(IsInCorridor(transform.TransformPoint(GetComponent<BoxCollider>().center)) == true){
			inWall = false;
			wallsEntered.Remove (collider as BoxCollider);
			//}
		}
	}

	public void ShowOutOfBoundsObjects(bool show){
		foreach (GameObject o in outOfBoundsObjects) {
			o.SetActive (show);
		}
		if (show == true) {
			RenderSettings.skybox = outOfBoundsSkybox;
		} else {
			RenderSettings.skybox = worldSkybox;
		}
	}
}

public class SolidWall_1: MonoBehaviour{

	[HideInInspector]
	public bool crossedWall, inWall, checkCrossBack;
	//[HideInInspector]
	public GameObject currentCorridor;
	[HideInInspector]
	//public GameObject grid;
	public GameObject[] panoramas;
	protected List<BoxCollider> wallsEntered;
	//[HideInInspector]
	public BoxCollider overlapCollider;
	public GameObject returnTrig;

	protected bool IsInCorridor(Vector3 colliderCenter){
		BoxCollider[] corridorColliders = currentCorridor.GetComponents<BoxCollider> ();
		for (int c = 0; c < corridorColliders.Length; c++) {
			if (corridorColliders [c].bounds.Contains (colliderCenter)) {
				return true;
			}
		}
		return false;
	}

	protected bool IsInReturnTrigger(BoxCollider collider){
		Vector3 center = transform.TransformPoint (collider.center);
		if (returnTrig.GetComponent<BoxCollider>().bounds.Contains (center)) {
			return true;
		}
		return false;
	}

	protected List<BoxCollider> CheckStayingInWall(List<BoxCollider> enteredWalls){
		for (int c = enteredWalls.Count-1; c>=0; c--) {
			if (enteredWalls [c].bounds.Intersects (GetComponent<BoxCollider>().bounds) == false) {
				enteredWalls.RemoveAt (c);
			}
		}
		return enteredWalls;
	}

	protected void ShowOnlyOneCorridor(Transform cell){
		BoxCollider[] corridorColliders = currentCorridor.GetComponents<BoxCollider> ();

		Collider[] keepColliders = null; //corridor colliders to render
		//piece to show whole corridor:
		//find objects to show
		/*for (int c = 0; c < corridorColliders.Length; c++) {
			Collider[] overlappedColls = 
				Physics.OverlapBox (currentCorridor.transform.TransformPoint(corridorColliders [c].center), 
					corridorColliders [c].size/2f);
			
			if (keepColliders == null) {
				keepColliders = new Collider[overlappedColls.Length];
				int i = 0;
				foreach (Collider obj in overlappedColls)
					keepColliders[i++] = obj;
			} else {
				Collider[] temp = new Collider[keepColliders.Length];
				temp = keepColliders;
				keepColliders = new Collider[temp.Length + overlappedColls.Length];
				int i = 0;
				foreach (Collider obj in temp)
					keepColliders[i++] = obj; 
				foreach (Collider obj in overlappedColls)
					keepColliders [i++] = obj;
			}
		}*/
		////

		//show one wall and one floor
		overlapCollider.transform.position = transform.position;

		Collider[] overlappedColls = 
			Physics.OverlapBox (transform.TransformPoint(overlapCollider.center), overlapCollider.size/2f);
		keepColliders = new Collider[overlappedColls.Length];
		int i = 0;
		foreach (Collider obj in overlappedColls)
			keepColliders[i++] = obj;

		//show return_trig

		for (int c = 0; c < keepColliders.Length; c++) {
			if (keepColliders [c].transform.position.y < 0.1f) {
				if (IsInCorridor (keepColliders [c].transform.position) == true) {
					returnTrig.transform.position = keepColliders [c].transform.position;
					break;
				}
			}
		}
		returnTrig.SetActive(true);

		//hide cell
		foreach (Transform child in cell)
		{
			if (child != cell){
				child.gameObject.SetActive (false);
			}
		}
		//show the corridor
		/*for (int c = 0; c < keepColliders.Length; c++) {
			keepColliders [c].gameObject.SetActive (true);
		}*/
		if (panoramas != null) {
			foreach (GameObject p in panoramas) {
				p.SetActive (false);
			}
		}
	}

	protected void ShowCell(Transform cell){
		foreach (Transform child in cell) {
			if (child != cell/* && child != currentCorridor.transform*/) {//********
				child.gameObject.SetActive (true);
			}
		}
		if (panoramas != null) {
			foreach (GameObject p in panoramas) {
				p.SetActive (true);
			}
		}
		returnTrig.SetActive(false);
	}



}
