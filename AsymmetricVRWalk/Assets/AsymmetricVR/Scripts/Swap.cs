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

	//visualize swaps in scene
	void OnDrawGizmos() {
		Gizmos.color = new Color(1f,1f,0f,0.5f);
		if (transform.rotation.eulerAngles.y < 45f) {
			// position of the drawCube (trigger_position, boxcollider_sizeX, y boxcollider_sizeZ)
			Gizmos.DrawCube (transform.position, 
				new Vector3 (GetComponent<BoxCollider> ().size.x, 0.1f, GetComponent<BoxCollider> ().size.z));
		}
		else /*if (transform.rotation.eulerAngles.y == 90f) */{
			Gizmos.DrawCube (transform.position, 
				new Vector3 (GetComponent<BoxCollider> ().size.z, 0.1f, GetComponent<BoxCollider> ().size.x));
		}

	}
}
