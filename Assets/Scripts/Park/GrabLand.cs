using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabLand : MonoBehaviour
{

    public List<GameObject> NearObject;
    GameObject ConnectedObject;
    bool Object_gripped = false;

    public List<GameObject> NearWorld;
    GameObject ConnectedWorld;
    bool World_gripped = false;
    bool rotatingLeft = false;
    bool rotatingRight = false;

    private void Update()
    {
        GripAndRelease();
        if (World_gripped)
        {
            //position
            float x_hand = this.gameObject.transform.position.x;
            float z_hand = this.gameObject.transform.position.z;
            float y_object = ConnectedWorld.transform.position.y;
            ConnectedWorld.transform.position = new Vector3(x_hand, y_object, z_hand);

            //rotation
            bool snapLeftAction = SteamVR_Actions.default_SnapTurnLeft[SteamVR_Input_Sources.RightHand].state;
            bool snapRightAction = SteamVR_Actions.default_SnapTurnRight[SteamVR_Input_Sources.RightHand].state;

            if (snapLeftAction && !rotatingLeft)
            {
                    ConnectedWorld.transform.Rotate(0, 0, 90);
                    rotatingLeft = true;
            }

            if (!snapLeftAction)
            {
                rotatingLeft = false;
            }

            if (snapRightAction && !rotatingRight)
            {
                ConnectedWorld.transform.Rotate(0, 0, -90);
                rotatingRight = true;
            }

            if (!snapRightAction)
            {
                rotatingRight = false;
            }

        }
        if (Object_gripped)
        {
            //position
            float x_hand = this.gameObject.transform.position.x;
            float z_hand = this.gameObject.transform.position.z;
            float y_object = ConnectedObject.transform.position.y;
            ConnectedObject.transform.position = new Vector3(x_hand, y_object, z_hand);

            //rotation
            bool snapLeftAction = SteamVR_Actions.default_SnapTurnLeft[SteamVR_Input_Sources.RightHand].state;
            bool snapRightAction = SteamVR_Actions.default_SnapTurnRight[SteamVR_Input_Sources.RightHand].state;

            if (snapLeftAction)
                ConnectedObject.transform.Rotate(0, 1, 0);
            else if (snapRightAction)
                ConnectedObject.transform.Rotate(0, 1, 0);
        }
    }

    private void GripAndRelease()
    {
        bool trigger = SteamVR_Actions.default_GrabGrip[SteamVR_Input_Sources.RightHand].state;
        bool pinch = SteamVR_Actions.default_GrabPinch[SteamVR_Input_Sources.RightHand].state;
        if (trigger)
        {
            GameObject NewWorld = ClosestWorldGrabbable();
            if (NewWorld != null)
            {
                ConnectedWorld = ClosestWorldGrabbable();//find the Closest Grabbable and set it to the connected objectif it isn't null
                World_gripped = true;
            }
        }
        else
        {
            ConnectedWorld = null;
            World_gripped = false;
        }
        if (pinch)
        {
            GameObject NewObject = ClosestObjectGrabbable();
            if (NewObject != null)
            {
                ConnectedObject = ClosestObjectGrabbable();//find the Closest Grabbable and set it to the connected objectif it isn't null
                Object_gripped = true;
            }
        }
        else
        {
            ConnectedObject = null;
            Object_gripped = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Add grabbable objects in range of our hand to a list
        if (other.CompareTag("Land") || other.CompareTag("Water"))
        {
            if (!World_gripped)
            {
                NearWorld.Add(other.gameObject);
            }
        }
        if (other.CompareTag("Object"))
        {
            if (!Object_gripped)
            {
                NearObject.Add(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //remove grabbable objects going out of range from our list
        if (other.CompareTag("Land") || other.CompareTag("Water"))
        {
            NearWorld.Remove(other.gameObject);
        }
        if (other.CompareTag("Object"))
        {
            NearObject.Remove(other.gameObject);
        }
    }

    GameObject ClosestWorldGrabbable()
    {
        //find the object in our list of grabbable that is closest and return it.
        GameObject ClosestGameObj = null;
        float Distance = float.MaxValue;
        if (NearWorld != null)
        {
            foreach (GameObject GameObj in NearWorld)
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

    GameObject ClosestObjectGrabbable()
    {
        //find the object in our list of grabbable that is closest and return it.
        GameObject ClosestGameObj = null;
        float Distance = float.MaxValue;
        if (NearObject != null)
        {
            foreach (GameObject GameObj in NearObject)
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
        NearObject = new List<GameObject>();
        ConnectedWorld = new GameObject();
        NearWorld = new List<GameObject>();
    }

    
}