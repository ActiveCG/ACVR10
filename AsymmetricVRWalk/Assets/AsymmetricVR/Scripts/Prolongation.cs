using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prolongation : MonoBehaviour {

	public float x_plus;
	public float z_plus;
	public float x_start;
	public float x_end;
	public float x_prolongStart;
	public float x_prolongEnd;
	[HideInInspector]
	public bool addedToOffset;

	void Start(){
		addedToOffset = false;
	}
}
