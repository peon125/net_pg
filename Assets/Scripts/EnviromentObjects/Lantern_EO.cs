using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern_EO : EnviromentObject
{
    public override void Grab(Hand hand)
    {
        handIAmGrabbedBy = hand;
        isGrabbed = true;

        StartCoroutine(Move(handIAmGrabbedBy.myVirtualHand, new Vector3(0, 4, 7)));
    }

    public override void Degrab()
    {
        Dehighlight();

        isGrabbed = false;

        StartCoroutine(Move(handIAmGrabbedBy.myVirtualHand, new Vector3(0, 0, 8.5f)));

        handIAmGrabbedBy = null;
    }
}
