using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALinkBetweenWorlds : MonoBehaviour
{

    public GameObject ObjetReel;


    void Awake()
    {
        setInitialPos();
    }

    
    void Update()
    {
        //Confinement (lol) à l'intérieur de la pièce
        //Coordonnées de la pièce : x = [-2.5,2.5], z = [-4,4]
        //On va laisser une marge de 0.5 de chaque côté

        Vector3 c = gameObject.transform.localPosition;
        if (c.x < -2)
            c.x = -2;
        if (c.x > 2)
            c.x = 2;
        if (c.z < -3.5)
            c.z = -3.5f;
        if (c.z > 3.5)
            c.z = 3.5f;

        gameObject.transform.localPosition = c;

        applyPosChanges();
    }

    //Conversions : Attention !
    //Le (0;0) de l'objet réel au centre de la pièce
    //Le (0;0) de l'objet WiM est le centre du plan "Sol"

    protected void setInitialPos()
    {
        //Étape 1 : Décalage entre le (0;0) de l'objet réel et le (0;0) de la pièce (situé au centre du monde parce que c'est plus facile vu que c'est littéralement de vecteur reelCoords en World Coordinates)
        //Étape 2 : En appliquant le même décalage à l'objet WiM dans le référentiel WiM, on obtient la même position par rapport au centre du plan que l'objet réel par rapport au centre de la pièce
        //          Cela est dû au fait que le GameObject Empty "WiM" est scale down, donc ses enfants le sont intrinsèquement

        Vector3 wimCoords = ObjetReel.transform.position;

        gameObject.transform.localPosition = wimCoords;
        gameObject.transform.localRotation = ObjetReel.transform.localRotation;
    }

    protected void applyPosChanges()
    {
        //Concrètement c'est pareil que l'autre fonction mais à l'envers en oubliant pas de garder le "y" initial.

        Vector3 reelCoords = gameObject.transform.localPosition;
        

        ObjetReel.transform.position = reelCoords;
        ObjetReel.transform.localRotation = gameObject.transform.localRotation;
    }
}
