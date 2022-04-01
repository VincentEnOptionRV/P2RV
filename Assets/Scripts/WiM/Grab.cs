using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grab : MonoBehaviour
{
    
    public List<GameObject> NearObjects;
    public GameObject ConnectedObject;
    bool Obj_gripped = false;
    private void Update()
    {
        GripAndRelease();
        if (Obj_gripped)
        {
            //position
            float x_hand = gameObject.transform.position.x;
            float z_hand = gameObject.transform.position.z;
            float y_object = ConnectedObject.transform.position.y;
            ConnectedObject.transform.position = new Vector3(x_hand, y_object, z_hand);

            //rotation
            bool snapLeftAction = SteamVR_Actions.default_SnapTurnLeft[SteamVR_Input_Sources.RightHand].state;
            bool snapRightAction = SteamVR_Actions.default_SnapTurnRight[SteamVR_Input_Sources.RightHand].state;

            if (snapLeftAction)
                ConnectedObject.transform.Rotate(0, 1, 0);
            else if (snapRightAction)
                ConnectedObject.transform.Rotate(0, -1, 0);
        }
    }

    private void GripAndRelease()
    {
        bool trigger = SteamVR_Actions.default_GrabGrip[SteamVR_Input_Sources.RightHand].state;
        if (trigger && !Obj_gripped)
        {
            GameObject NewObject = ClosestGrabbable();
            if (NewObject != null)
            {
                ConnectedObject = ClosestGrabbable();//find the Closest Grabbable and set it to the connected objectif it isn't null
                Obj_gripped = true;
            }
        }
        else if (!trigger)
        {
            ConnectedObject = null;
            Obj_gripped = false;
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Add grabbable objects in range of our hand to a list
        if (other.CompareTag("Grabbable"))
        {
            if (!Obj_gripped)
            {
                NearObjects.Add(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //remove grabbable objects going out of range from our list
        if (other.CompareTag("Grabbable"))
        {
            NearObjects.Remove(other.gameObject);
        }
    }

    public GameObject ClosestGrabbable()
    {
        //find the object in our list of grabbable that is closest and return it.
        GameObject ClosestGameObj = null;
        float Distance = float.MaxValue;
        if (NearObjects != null)
        {
            foreach (GameObject GameObj in NearObjects)
            {
                if ((GameObj.transform.position - transform.position).sqrMagnitude < Distance)
                {
                    ClosestGameObj = GameObj;
                    Distance = (GameObj.transform.position - transform.position).sqrMagnitude;
                }
            }
        }
        return ClosestGameObj;
    }

    private void Start()
    {
        ConnectedObject = new GameObject();
        NearObjects = new List<GameObject>();
        
    }
    

    
}
