using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_EO : EnviromentObject
{
    void Update()
    {
        if (!isGrabbed)
            return;

        stopCoroutine = false;

        if (beingUsed)
        {
            handIAmGrabbedBy.myVirtualHand.transform.localPosition =new Vector3(0, 1.75f, 7f);
        }
        else
        {
            handIAmGrabbedBy.myVirtualHand.transform.localPosition = new Vector3(0, 2.5f, 3.5f);
        }
    }

    public override void Grab(Hand hand)
    {
        handIAmGrabbedBy = hand;
        isGrabbed = true;

        StartCoroutine(Move(handIAmGrabbedBy.myVirtualHand, new Vector3(0, 2.5f, 3.5f)));
    }

    public override void Degrab()
    {
        Dehighlight();

        isGrabbed = false;

        StartCoroutine(Move(handIAmGrabbedBy.myVirtualHand, new Vector3(0, 0, 8.5f)));

        handIAmGrabbedBy = null;
    }
}