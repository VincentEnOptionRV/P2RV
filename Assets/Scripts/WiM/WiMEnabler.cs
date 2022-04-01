using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WiMEnabler : MonoBehaviour
{
    public GameObject wim;
    protected bool alreadyGripping = false;
    // Start is called before the first frame update
    void Start()
    {
        wim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool grip = SteamVR_Actions.default_GrabGrip[SteamVR_Input_Sources.LeftHand].state;
        if (grip)
        {
            if (!alreadyGripping)
            {
                wim.SetActive(!wim.activeInHierarchy);
                alreadyGripping = true;
            }
        }
        else
        {
            alreadyGripping = false;
        }
    }
}
