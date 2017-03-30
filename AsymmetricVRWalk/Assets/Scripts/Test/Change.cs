using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour 
{
	public GameObject world;
	public GameObject disSquare;
	public GameObject disNew;
	private bool state = false;
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			print("Hey");
			if(state == false)
			{
				world.SetActive(true);
				disSquare.SetActive(false);
			if(disNew != null)
			{
				disNew.SetActive(false);
			}

			state = true;
			}
			else if(state == true)
			{
				disSquare.SetActive(true);
				world.SetActive(false);
			if(disNew != null)
			{
				disNew.SetActive(true);
			}
			state = false;
			}
		}
	}
}
