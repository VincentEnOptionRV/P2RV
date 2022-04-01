using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALinkBetweenWolrdsPlayer : MonoBehaviour
{
    public GameObject ObjetReel;
    bool allowed = true;
    bool fixated = true;

    void Awake()
    {
        setInitialPos();
    }


    void Update()
    {
        fixated = gameObject.transform.parent.parent.gameObject.GetComponent<WIM>().fixated;

        Vector3 c = gameObject.transform.localPosition;
        if (c.x < -4)
            c.x = -4;
        if (c.x > 4)
            c.x = 4;
        if (c.z < -4)
            c.z = -4;
        if (c.z > 4)
            c.z = 4;

        gameObject.transform.localPosition = c;

        if (allowed && !fixated)
        {
            applyPosChanges();
        }
    }

    protected void setInitialPos()
    {
        Vector3 wimCoords = ObjetReel.transform.position;

    }

    protected void applyPosChanges()
    {
        Vector3 reelCoords = gameObject.transform.localPosition;
        Vector3 intCoords = new Vector3();

        intCoords.x = (int)reelCoords.x;
        intCoords.y = 0;
        intCoords.z = (int)reelCoords.z;

        reelCoords.y = ObjetReel.transform.position.y / 8;

        ObjetReel.transform.position = 8*(reelCoords - 0.1f*intCoords) + new Vector3(10.15f, 0, 3.29f);
    }

    void OnTriggerStay(Collider other)
    {
        //Add grabbable objects in range of our hand to a list
        if (other.CompareTag("Land"))
        {
            allowed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Land"))
        {
            allowed = false;
        }
    }
}