using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown("w"))
        {
            transform.position -= new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKeyDown("s"))
        {
            transform.position += new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKeyDown("d"))
        {
            transform.position += new Vector3(0, 0, 0.1f);
        }
        if (Input.GetKeyDown("a"))
        {
            transform.position -= new Vector3(0, 0, 0.1f);
        }
    }
}
