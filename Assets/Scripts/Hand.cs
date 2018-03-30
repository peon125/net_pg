using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform myVirtualHand;

    [SerializeField]
    GameObject handJoint_gameObject;
    [SerializeField]
    Joint handJoint;

    Rigidbody selectedObject;
    List<Coroutine> coroutines_list = new List<Coroutine>();
    bool catched = false;

    void OnTriggerEnter(Collider other)
    {
        EnviromentObject eo = other.GetComponentInParent<EnviromentObject>();

        if (!eo || !eo.grabbable)
            return;

        Rigidbody othersRb = other.GetComponentInParent<Rigidbody>();

        if (catched || !othersRb)
            return;

        selectedObject = othersRb;

        selectedObject.GetComponentInParent<EnviromentObject>().Highlight();
    }

    void OnTriggerExit(Collider other)
    {
        EnviromentObject eo = other.GetComponentInParent<EnviromentObject>();

        if (!eo || !eo.grabbable)
            return;

        Rigidbody othersRb = other.GetComponentInParent<Rigidbody>();

        if (catched || !othersRb)
            return;

        if (othersRb == selectedObject)
        {
            selectedObject.GetComponentInParent<EnviromentObject>().Dehighlight();
        }

        selectedObject = null;
    }

    void Update()
    {
        if (!catched && myVirtualHand.localPosition != new Vector3(0, 0, 8.5f))
        {
            myVirtualHand.localPosition = new Vector3(0, 0, 8.5f);
        }
    }

    public void Catch()
    {
        if (!selectedObject)
        {
            handJoint.gameObject.SetActive(false);
        }
        else if (!catched)
        {
            selectedObject.GetComponentInParent<EnviromentObject>().Grab(this);
            catched = true;
            handJoint.gameObject.SetActive(true);
            handJoint.connectedBody = selectedObject;
        }
    }

    public void Decatch()
    {
        catched = false;

        if (selectedObject)
        {
            selectedObject.GetComponentInParent<EnviromentObject>().Degrab();
        }

        selectedObject = null;
        handJoint.connectedBody = null;
        handJoint.gameObject.SetActive(false);
    }
}