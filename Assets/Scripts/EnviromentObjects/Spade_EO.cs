using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spade_EO : EnviromentObject
{
    void Update()
    {
        if (!isGrabbed)
            return;

        if (Input.GetKey(KeyCode.G))
        {
            stopCoroutine = true;

            Debug.Log(handIAmGrabbedBy.myVirtualHand.name);
            handIAmGrabbedBy.myVirtualHand.transform.localPosition = Vector3.Lerp(handIAmGrabbedBy.myVirtualHand.localPosition, new Vector3(0, 1.75f, 7f), 20 * Time.deltaTime);
        }
        else
        {
            stopCoroutine = false;

            handIAmGrabbedBy.myVirtualHand.transform.localPosition = Vector3.Lerp(handIAmGrabbedBy.myVirtualHand.localPosition, new Vector3(0, 2.5f, 3.5f), 20 * Time.deltaTime);
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
        Debug.Log("nom");

        isGrabbed = false;

        StartCoroutine(Move(handIAmGrabbedBy.myVirtualHand, new Vector3(0, 0, 8.5f)));

        handIAmGrabbedBy = null;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("bubble"))
        {
            Destroy(other.gameObject);
        }
    }
}