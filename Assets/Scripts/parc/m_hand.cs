using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class m_hand : MonoBehaviour
{
    public SteamVR_Action_Boolean grap =null;
    public SteamVR_Behaviour_Pose pose= null;  // to get the input source 
    private InteractableObject current_ob;
    private List<InteractableObject> intercatble_objects = new List<InteractableObject>();
    private FixedJoint joint =null ;


    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();
    }


    void Update()
    {
        getNearInteractables();
        if (grap.GetStateUp(pose.inputSource))
        {
            pick();
        }
    }

    public void pick()
    {
        InteractableObject near_ob = getNearInteractables();

        if (!near_ob) { return; }

        if (near_ob.hand_holding)
        {
            near_ob.hand_holding.release();
        }

        near_ob.transform.position = this.transform.position;

        joint.connectedBody = near_ob.gameObject.GetComponent<Rigidbody>();

        near_ob.hand_holding = this;
    }

    public void release()
    {

        if (!current_ob)
        {
            return;
        }

        joint.connectedBody = null;

        Rigidbody rb = current_ob.GetComponent<Rigidbody>();
        rb.velocity = pose.GetVelocity();
        rb.angularVelocity = pose.GetAngularVelocity();

        current_ob.hand_holding = null;
        current_ob = null;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grapable"))
        {
            intercatble_objects.Add(other.gameObject.GetComponent<InteractableObject>());
            current_ob = other.gameObject.GetComponent<InteractableObject>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Grapable"))
        {
            intercatble_objects.Remove(other.gameObject.GetComponent<InteractableObject>());
        }
    }

    public InteractableObject getNearInteractables()
    {
        InteractableObject nearest_object = new InteractableObject();
        var min = Mathf.Abs(Vector3.Distance(transform.position ,current_ob.transform.position));
        foreach(InteractableObject ob in intercatble_objects)
        {
            if(Mathf.Abs(Vector3.Distance(transform.position, ob.transform.position)) < min)
            {
                nearest_object = ob;
                min = Mathf.Abs(Vector3.Distance(transform.position, ob.transform.position));
            }
        }
        return nearest_object;
    }
}
