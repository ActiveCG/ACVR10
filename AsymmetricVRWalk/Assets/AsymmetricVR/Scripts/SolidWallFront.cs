using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidWallFront : SolidWall {

	public SolidWallSide leftCollider;
	public SolidWallSide rightCollider;

	void Start () {
		crossedWall = false;
		inWall = false;
		checkCrossBack = false;
		wallsEntered = new List<BoxCollider> ();
		currentCorridor = GameObject.Find ("Cell1/corridor1");
		leftCollider.currentCorridor = currentCorridor;
		rightCollider.currentCorridor = currentCorridor;
		grid = GameObject.FindGameObjectWithTag ("Grid");
		leftCollider.grid = grid;
		rightCollider.grid = grid;

		ShowGrid (grid, false);
	}


	void Update () {
		if (checkCrossBack == true) {
			if (IsInCorridor (transform.TransformPoint (GetComponent<BoxCollider> ().center)) == true) {
				ShowCell (GetComponent<HMD_user>().currentCell.transform);
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
			if (crossedWall == true && collider.gameObject == currentCorridor) {
				checkCrossBack = true;
			} 
			//entering next corridor
			//if front not in wall and hasn't crossed wall and the left and right colliders have not crossed wall
			else if (crossedWall == false && inWall == false
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

public class SolidWall: MonoBehaviour{

	[HideInInspector]
	public bool crossedWall, inWall, checkCrossBack;
	[HideInInspector]
	public GameObject currentCorridor;
	[HideInInspector]
	public GameObject grid;
	protected List<BoxCollider> wallsEntered;

	protected bool IsInCorridor(Vector3 colliderCenter){
		BoxCollider[] corridorColliders = currentCorridor.GetComponents<BoxCollider> ();
		for (int c = 0; c < corridorColliders.Length; c++) {
			if (corridorColliders [c].bounds.Contains (colliderCenter)) {
				return true;
			}
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
		//find objects to show
		for (int c = 0; c < corridorColliders.Length; c++) {
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
		}

		//hide cell
		foreach (Transform child in cell)
		{
			if (child != cell){
				child.gameObject.SetActive (false);
			}
		}
		//show the corridor
		for (int c = 0; c < keepColliders.Length; c++) {
			keepColliders [c].gameObject.SetActive (true);
		}
	}

	protected void ShowCell(Transform cell){
		foreach (Transform child in cell) {
			if (child != cell && child != currentCorridor.transform) {//********
				child.gameObject.SetActive (true);
			}
		}
	}

	protected void ShowGrid(GameObject grid, bool show){
		grid.SetActive (show);
	}
}
