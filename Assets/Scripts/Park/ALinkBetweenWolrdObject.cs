using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALinkBetweenWolrdObject : MonoBehaviour
{
    public GameObject ObjetReel;
    public GameObject plan;

    void Awake()
    {
        setInitialPos();
    }


    void Update()
    {
        //Confinement (lol) � l'int�rieur de la pi�ce
        //Coordonn�es de la pi�ce : x = [-2.5,2.5], z = [-4,4]
        //On va laisser une marge de 0.5 de chaque c�t�

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

    //Conversions : Attention !
    //Le (0;0) de l'objet r�el au centre de la pi�ce
    //Le (0;0) de l'objet WiM est le centre du plan "Sol"

    protected void setInitialPos()
    {
        //�tape 1 : D�calage entre le (0;0) de l'objet r�el et le (0;0) de la pi�ce (situ� au centre du monde parce que c'est plus facile vu que c'est litt�ralement de vecteur reelCoords en World Coordinates)
        //�tape 2 : En appliquant le m�me d�calage � l'objet WiM dans le r�f�rentiel WiM, on obtient la m�me position par rapport au centre du plan que l'objet r�el par rapport au centre de la pi�ce
        //          Cela est d� au fait que le GameObject Empty "WiM" est scale down, donc ses enfants le sont intrins�quement

        Vector3 wimCoords = ObjetReel.transform.position;

        //gameObject.transform.localPosition = wimCoords;
        //gameObject.transform.localRotation = ObjetReel.transform.localRotation;
    }

    protected void applyPosChanges()
    {
        Vector3 reelCoords = plan.transform.InverseTransformPoint(transform.TransformPoint(gameObject.transform.localPosition));
        Vector3 intCoords = new Vector3();

        intCoords.x = (int)reelCoords.x;
        intCoords.y = 0;
        intCoords.z = (int)reelCoords.z;

        reelCoords.y = ObjetReel.transform.position.y / 8;

        ObjetReel.transform.position = 8 * (reelCoords - 0.1f*intCoords) + new Vector3(10.15f, 0, 3.29f);
    }

    void OnTriggerEnter(Collider other)
    {
        //Add grabbable objects in range of our hand to a list
        if (other.CompareTag("Land"))
        {
            gameObject.transform.SetParent(other.gameObject.transform);
        }
    }
}