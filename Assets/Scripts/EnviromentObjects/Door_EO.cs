using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_EO : EnviromentObject
{
    [SerializeField]
    Transform door;

    bool did = false;

    public override void Degrab()
    {
        Dehighlight();

        isGrabbed = false;

        handIAmGrabbedBy = null;
    }

    public override void Grab(Hand hand)
    {
        handIAmGrabbedBy = hand;
        isGrabbed = true;
    }

    private void Update()
    {
        if (!isGrabbed)
            return;

        if (beingUsed)
        {
            handIAmGrabbedBy.myVirtualHand.transform.localPosition = new Vector3(0, 0, 4f);
        }
        else
        {
            handIAmGrabbedBy.myVirtualHand.transform.localPosition = new Vector3(0, 0, 8.5f);
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(door.localEulerAngles, Vector3.zero) < 3)
        {
            if (!did)
            {
                did = true;
                door.GetComponent<Rigidbody>().velocity = Vector3.zero;
                door.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
        else if (Vector3.Distance(door.localEulerAngles, Vector3.zero) > 15)
        {
            did = false;
        }
    }
}