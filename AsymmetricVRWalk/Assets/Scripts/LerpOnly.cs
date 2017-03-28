using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpOnly : MonoBehaviour
{
    private GameObject player;

    private bool runOnce = false;

    [HideInInspector]
    public Vector3 offset;

    private float speed;
    private float startTime;
    private bool playerFollow = true;
    private bool setupOnce = false;

    void Start()
    {
        speed = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
        startTime = Time.time;
    }

    void Update()
    {
            transform.position = LerpTrue(transform.position, player.transform.position + offset, speed);       
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "lerpTrig")
        {
            offset += transform.position - (player.transform.position + offset - new Vector3(col.GetComponent<MultiSpeed>().transitionLengthX, col.GetComponent<MultiSpeed>().transitionLengthY, col.GetComponent<MultiSpeed>().transitionLengthZ));
            playerFollow = false;
            //speed = col.GetComponent<MultiSpeed>().speed; LERPING IF NOT UNCOMMENTED STUFF BREAKS TEMP FIX
        }
    }

    Vector3 LerpTrue(Vector3 currPosition, Vector3 targetPosition, float speed)
    {
        float length = Vector3.Distance(currPosition, targetPosition);
        float distance = (Time.time - startTime) * speed;
        float journey = distance / length;
        return Vector3.Lerp(currPosition, targetPosition, journey);
    }
}
