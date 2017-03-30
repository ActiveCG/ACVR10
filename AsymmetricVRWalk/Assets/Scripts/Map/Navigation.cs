using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{

    private GameObject target;
    private NavMeshAgent nav;

    public static Vector3 offset;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();

        offset = transform.position - target.transform.position;
    }

    void Update()
    {
        nav.SetDestination(target.transform.position + offset);
    }

    public void UpdateOffset(float x, float y, float z)
    {
        offset += transform.position - ((target.transform.position + offset) + new Vector3(x * 0.635f, y * 0.635f, z * 0.635f));
    }
}
