using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public GameObject main;

    // Update is called once per frame
    void Update()
    {
        MeshRenderer mr = gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        mr.enabled = gameObject == main.GetComponent<Grab>().ClosestGrabbable();
    }
}
