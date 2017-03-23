using System.Collections;
using UnityEngine;

public class HMD_user : MonoBehaviour {

	[HideInInspector]
	public GameObject currentCell;

	void Start(){
		currentCell = GameObject.Find ("Cell1");
	}
}
