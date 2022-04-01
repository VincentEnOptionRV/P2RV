using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class gripme : MonoBehaviour
{
    public GameObject Righthand;
    public SteamVR_Action_Boolean Grip_button;
    private bool graped;
    public SteamVR_Input_Sources righthand;
    private bool rotation;


    private void Start()
    {
        graped = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (graped)
        {
            // si on clique sur grippinch
            if (Grip_button.GetStateDown(righthand))
            {
                graped = false;
            }
        }
        else
        {
            if (Grip_button.GetStateDown(righthand))
            {
                gameObject.transform.position = Righthand.transform.position;
                gameObject.transform.rotation = Righthand.transform.rotation;
            }
        }

    }
}
