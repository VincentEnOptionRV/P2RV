using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlinkBetweenWorldLand : MonoBehaviour
{
    public GameObject ObjetReel;


    void Awake()
    {
        setInitialPos();
    }


    void Update()
    {
        //on restrreint les position au plan

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
        //En fait fonction qui sert à rien

        Vector3 wimCoords = ObjetReel.transform.position;

    }

    protected void applyPosChanges()
    {
        //copie des coordonnées de l'objet du wim
        Vector3 reelCoords = gameObject.transform.localPosition;
        
        //conversion pour simuler un quadrillage
        reelCoords.x = (int)reelCoords.x;
        reelCoords.z = (int)reelCoords.z;

        //copie pour placer l'objet dans le wim (plaques séparées par des espaces)
        Vector3 wimCoords = reelCoords;

        reelCoords.y = ObjetReel.transform.position.y / 8;

        ObjetReel.transform.position = 8*reelCoords + new Vector3(10.15f ,0 ,3.29f );
        wimCoords.x = wimCoords.x + 0.1f * wimCoords.x;
        wimCoords.z = wimCoords.z + 0.1f * wimCoords.z;
        gameObject.transform.localPosition = wimCoords;
        ObjetReel.transform.localRotation = gameObject.transform.localRotation;
    }
}