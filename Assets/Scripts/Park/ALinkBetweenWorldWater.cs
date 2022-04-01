using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALinkBetweenWorldWater : MonoBehaviour
{
    public GameObject ObjetReel;

    void Awake()
    {
        //setInitialPos();
    }


    void Update()
    {
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

            applyPosChanges();
    }

    protected void setInitialPos()
    {
        Vector3 wimCoords = ObjetReel.transform.position;

    }

    protected void applyPosChanges()
    {

        Vector3 reelCoords = gameObject.transform.localPosition;
        Vector3 pos_reelCoords = new Vector3();
        Vector3 decalage = new Vector3();

        pos_reelCoords.x = (int)reelCoords.x;
        pos_reelCoords.z = (int)reelCoords.z;

        Vector3 wimCoords = pos_reelCoords;
        if (reelCoords.x >= pos_reelCoords.x + 0.1f * pos_reelCoords.x)
        {
            wimCoords.x = wimCoords.x + 0.1f * wimCoords.x + 0.275f;
            decalage.x = 12.15f;
        }

        if (reelCoords.x < pos_reelCoords.x + 0.1f * pos_reelCoords.x)
        {
            wimCoords.x = wimCoords.x + 0.1f * wimCoords.x - 0.275f;
            decalage.x = 8.15f;
        }

        if (reelCoords.z >= pos_reelCoords.z + 0.1f * pos_reelCoords.z)
        {
            wimCoords.z = wimCoords.z + 0.1f * wimCoords.z + 0.275f;
            decalage.z = 5.29f;
        }
        if (reelCoords.z < pos_reelCoords.z + 0.1f * pos_reelCoords.z)
        {
            wimCoords.z = wimCoords.z + 0.1f * wimCoords.z - 0.275f;
            decalage.z = 1.29f;
        }

        pos_reelCoords.y = ObjetReel.transform.position.y / 8;

        ObjetReel.transform.position = 8 * pos_reelCoords + decalage;
        gameObject.transform.localPosition = wimCoords;
        //ObjetReel.transform.localRotation = gameObject.transform.localRotation;
    }
}