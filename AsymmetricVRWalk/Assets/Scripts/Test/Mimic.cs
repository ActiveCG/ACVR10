using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : MonoBehaviour
{

    private GameObject player;

    [HideInInspector]
    public Vector3 offset;
    [HideInInspector]
    public Vector3 tempP;

    public float scalarX;
    public float scalarZ;
    public float scalarY;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        tempP.x *= scalarX;
        tempP.y *= scalarY;
        tempP.z *= scalarZ;
        transform.position = new Vector3(player.transform.position.x * scalarX, player.transform.position.y * scalarY, player.transform.position.z * scalarZ) + offset;
    }
}