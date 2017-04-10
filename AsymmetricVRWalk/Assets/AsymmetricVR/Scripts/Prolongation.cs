using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProlongationType{
	BRIDGE, TUNNEL
}

public class Prolongation : MonoBehaviour {
	
	public float prolongedX; //how many tiles to add extra to the one in maze
	public float prolongedZ;
	public float tileStart;//in maze
	public float tileEnd;//in maze
	public float prolongationStart;//in map
	public float prolongationEnd;//in map
	public ProlongationType type;
	[HideInInspector]
	public bool addedToOffset;

	void Start(){
		addedToOffset = false;
	}
}
