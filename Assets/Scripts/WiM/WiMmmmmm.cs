using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class WiMmmmmm : MonoBehaviour
{
    public GameObject leftHand;
    public bool fixated = false;
    protected bool alreadyTriggering = false;

    void Update()
    {
        //Position
        bool trigger = SteamVR_Actions.default_GrabPinch[SteamVR_Input_Sources.LeftHand].state;
        if (trigger)
        {
            if (!alreadyTriggering)
            {
                fixated = !fixated;
                alreadyTriggering = true;
            }
        }
        else
        {
            alreadyTriggering = false;
        }

        if (!fixated)
            gameObject.transform.position = leftHand.transform.position + new Vector3(0,0.1f,0);
        
        
        //Rotation   
        bool snapLeftAction = SteamVR_Actions.default_SnapTurnLeft[SteamVR_Input_Sources.LeftHand].state;
        bool snapRightAction = SteamVR_Actions.default_SnapTurnRight[SteamVR_Input_Sources.LeftHand].state;
             
        if (snapLeftAction)
            gameObject.transform.Rotate(0, 1, 0);
        else if (snapRightAction)
            gameObject.transform.Rotate(0, -1, 0);

    }
}
