using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public float tileSize = 0.625f;

    private GameObject target;
    private NavMeshAgent nav;

    public static Vector3 offset;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = false;

        offset = transform.position - target.transform.position;
    }

    void Update()
    {
        nav.SetDestination(target.transform.position + offset);
        transform.rotation = target.transform.rotation;
    }

    public void UpdateOffset(float x, float y, float z)
    {
        offset += transform.position - ((target.transform.position + offset) + new Vector3(x * tileSize, y * tileSize, z * tileSize));
    }
}
