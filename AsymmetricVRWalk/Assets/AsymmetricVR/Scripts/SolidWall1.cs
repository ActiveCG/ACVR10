using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SolidWall1 : MonoBehaviour {

	private bool crossedWall;
	private bool inWall;
	GameObject cor;

	BoxCollider[] colls;


	// Use this for initialization
	void Start () {
		crossedWall = false;
		cor = GameObject.Find ("col1");
	}
	
	// Update is called once per frame
	void Update () {
		if (crossedWall == true) {

		}
	}

	void OnTriggerExit(Collider collider){
		if (collider.tag == "CorridorBox" && inWall == true && crossedWall == false && collider.gameObject == cor) {
			
			Debug.Log ("out");

			GameObject corridor = collider.gameObject;
			//cor = corridor;
			BoxCollider[] corridorColliders = corridor.GetComponents<BoxCollider> ();
			Debug.Log (corridorColliders.Length);
			Collider[] col = null;

			for (int c = 0; c < corridorColliders.Length; c++) {
				Collider[] overlappedColls = 
					Physics.OverlapBox (corridor.transform.TransformPoint(corridorColliders [c].center), 
						corridorColliders [c].size/2f);
				Debug.Log (overlappedColls.Length);
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
			//Collider[] overlappedColls = Physics.OverlapBox (corridor.transform.TransformPoint(corridorColliders [0].center), corridorColliders [0].size/2f);
			Transform cell = GameObject.Find ("Cell1").transform;
			foreach (Transform child in cell)
			{
				if (child != cell){
					child.gameObject.SetActive (false);
				}
			}
			for (int c = 0; c < col.Length; c++) {
				col [c].gameObject.SetActive (true);
			}

			colls = new BoxCollider[corridorColliders.Length];
			colls = corridorColliders;
			Debug.Log (colls);
			crossedWall = true;
		}

		if (collider.tag == "Wall") {
			inWall = false;
		}
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Wall") {
			inWall = true;
		}

		if (collider.tag == "CorridorBox"){
			if (crossedWall == true && collider.gameObject == cor) {
			
				Debug.Log ("back");

				Transform cell = GameObject.Find ("Cell1").transform;
				foreach (Transform child in cell) {
					if (child != cell) {
						child.gameObject.SetActive (true);
					}
				}

				crossedWall = false;
			} else if (crossedWall == false && inWall == false) {
				cor = collider.gameObject;
			}
		}
	}

	/*void OnDrawGizmos() {
		if (crossingWall == true) {
			foreach (BoxCollider c in colls) {
				Gizmos.color = new Color (1, 0, 0, 1);
				Gizmos.DrawCube (cor.transform.TransformPoint (c.center), c.size);
			}
		}
	}*/
}
