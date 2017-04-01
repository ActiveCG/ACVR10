using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicFollower : MonoBehaviour {

	private Transform hmd;
	public Transform refPoint;
	private float tileSize = 0.625f;

	private Prolongation currentProlong;
	private float tileStart;//physical
	private float tileEnd;
	private float prolongStart;//in map
	private float prolongEnd;
	private Vector3 prolongOffset;
	private bool inTunnelEntrance;//yo allow entrance in a tunnel

	void Start () {
		hmd = GameObject.FindGameObjectWithTag ("Player").transform;
		currentProlong = null;
		prolongOffset = Vector3.zero;
		inTunnelEntrance = false;
	}

	void Update () {
		if (currentProlong != null) {
			SpeedUp ();
		} else {
			transform.position = refPoint.position + hmd.position + prolongOffset;
			float y = Mathf.Clamp (hmd.transform.position.y, 0.5f, 2.6f);
			transform.position = new Vector3 (transform.position.x, y, transform.position.z);
		}

		transform.rotation = hmd.rotation;
	}

	private void SpeedUp(){
		Vector3 offset = Vector3.zero;
		if (currentProlong.prolongedX > 0) {
			float p = (hmd.position.x - tileStart) / (tileEnd - tileStart);
			float extra = 0f;
			if (p < 0f) {
				extra = hmd.position.x - tileStart;
			} else if (p > 1f) {
				extra = hmd.position.x - tileEnd;
			}
			p = Mathf.Clamp01 (p);
			float x_offset = prolongStart + p * (prolongEnd - prolongStart) + extra;
			x_offset -= prolongOffset.x;
			if (currentProlong.addedToOffset == true) {
				//x_offset -= 1f * (prolongEnd - prolongStart) * currentProlong.x_plus / (currentProlong.x_plus + 1f);
			}
			offset = new Vector3 (x_offset, hmd.position.y, hmd.position.z);
		}
		else if (currentProlong.prolongedZ > 0) {
			float p = (hmd.position.z - tileStart) / (tileEnd - tileStart);
			float extra = 0f;
			if (p < 0f) {
				extra = hmd.position.z - tileStart;
			} else if (p > 1f) {
				extra = hmd.position.z - tileEnd;
			}
			p = Mathf.Clamp01 (p);
			float z_offset = prolongStart + p * (prolongEnd - prolongStart) + extra;
			z_offset -= prolongOffset.z;
			if (currentProlong.addedToOffset == true) {
				//z_offset -= 1f * (prolongEnd - prolongStart) * currentProlong.z_plus / (currentProlong.z_plus + 1f);
			}
			offset = new Vector3 (hmd.position.x, hmd.position.y, z_offset);
		}
		transform.position = refPoint.position + prolongOffset + offset;
		float y = hmd.transform.position.y;
		if (currentProlong.type == ProlongationType.TUNNEL) {
			y = Mathf.Clamp (y, 0f, 0.3f);
		} else {
			y = Mathf.Clamp (y, 0.5f, 2.6f);
		}
		transform.position = new Vector3 (transform.position.x, y, transform.position.z);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "TunnelEntrance") {
			inTunnelEntrance = true;
		}
		if (col.GetComponent<Prolongation> () != null && currentProlong == null) {
			if ((col.GetComponent<Prolongation> ().type == ProlongationType.TUNNEL && inTunnelEntrance == true)
			   || col.GetComponent<Prolongation> ().type == ProlongationType.BRIDGE) {
				currentProlong = col.GetComponent<Prolongation> ();
				tileStart = currentProlong.tileStart;
				tileEnd = currentProlong.tileEnd;
				prolongStart = currentProlong.prolongationStart;
				prolongEnd = currentProlong.prolongationEnd;
				print ("in");
			}
		}
	}

	void OnTriggerStay(Collider col){
		if (col.GetComponent<Prolongation> () != null && currentProlong == null) {
			if ((col.GetComponent<Prolongation> ().type == ProlongationType.TUNNEL && inTunnelEntrance == true)
			    || col.GetComponent<Prolongation> ().type == ProlongationType.BRIDGE) {
				currentProlong = col.GetComponent<Prolongation> ();
				tileStart = currentProlong.tileStart;
				tileEnd = currentProlong.tileEnd;
				prolongStart = currentProlong.prolongationStart;
				prolongEnd = currentProlong.prolongationEnd;
				print ("in");
			}
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "TunnelEntrance") {
			inTunnelEntrance = false;
		}
		if (col.GetComponent<Prolongation> () != null && col.GetComponent<Prolongation>() == currentProlong) {
			if (currentProlong.prolongedX > 0) {
				float p = (hmd.position.x - tileStart) / (tileEnd - tileStart);
				p = Mathf.Clamp01 (p);
				print ("exit " + p);
				float x_offset = 1f * (prolongEnd - prolongStart) * currentProlong.prolongedX / (currentProlong.prolongedX + 1f);
				if (p == 0f && currentProlong.addedToOffset == true) {
					prolongOffset = new Vector3 (prolongOffset.x - x_offset, prolongOffset.y, prolongOffset.z);
					currentProlong.addedToOffset = false;
				} else if (p == 1f && currentProlong.addedToOffset == false) {
					prolongOffset = new Vector3 (prolongOffset.x + x_offset, prolongOffset.y, prolongOffset.z);
					currentProlong.addedToOffset = true;
				}
			}
			else if (currentProlong.prolongedZ > 0) {
				float p = (hmd.position.z - tileStart) / (tileEnd - tileStart);
				p = Mathf.Clamp01 (p);
				print ("exit " + p);
				float z_offset = 1f * (prolongEnd - prolongStart) * currentProlong.prolongedZ / (currentProlong.prolongedZ + 1f);
				if (p == 0f && currentProlong.addedToOffset == true) {
					prolongOffset = new Vector3 (prolongOffset.x, prolongOffset.y, prolongOffset.z - z_offset);
					currentProlong.addedToOffset = false;
				} else if (p == 1f && currentProlong.addedToOffset == false) {
					prolongOffset = new Vector3 (prolongOffset.x, prolongOffset.y, prolongOffset.z + z_offset);
					currentProlong.addedToOffset = true;
				}
			}
			currentProlong = null;
		}
	}
}
