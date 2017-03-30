using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMimic : MonoBehaviour {

    private GameObject player;

    [HideInInspector]
    public Vector3 offset;
    [HideInInspector]
    private Vector3 start;
    [HideInInspector]
    private Vector3 end;

    public float speed;
    public float transitionLength;

    private float startTime;
    private float length;
    private float journey;

    private bool check = false;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }
	
	void Update ()
    {
        if (check == false)
        {
            transform.position = player.transform.position + offset;
        }
        else if (check == true)
        {
            float distance = (Time.time - startTime) * speed;
            journey = distance / length;
            transform.position = Vector3.Lerp(start, start + new Vector3(transitionLength,0,0), journey);
        }
        if (journey >= 1 && check == true)
        {         
            offset += transform.position - (player.transform.position + offset);
            journey = 0;
            check = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "lerpTrig")
        {
            start = transform.position;
            startTime = Time.time;
            length = Vector3.Distance(start, start + new Vector3(transitionLength, 0, 0));
            
            check = true;
        }
    }
}
