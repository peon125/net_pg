using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform myVirtualHand;
    public string button_string;

    [SerializeField]
    GameObject handJoint_gameObject;
    [SerializeField]
    Joint handJoint;

    Rigidbody _selectedObject_rb;
    Rigidbody selectedObject_rb
    {
        get
        {
            return _selectedObject_rb;
        }

        set
        {
            _selectedObject_rb = value;

            if (value != null)
            {
                selectedObject_eo = value.GetComponentInParent<EnviromentObject>();
            }
            else
            {
                selectedObject_eo = null;
            }
        }
    }
    EnviromentObject selectedObject_eo;
    List<Coroutine> coroutines_list = new List<Coroutine>();
    bool catched = false;

    void Update()
    {
        if (Input.GetButton("EnableCatching"))
        {
            if (selectedObject_rb && !catched && Input.GetButtonDown(button_string + "Click"))
            {
                Catch();
            }
            else if (selectedObject_rb && catched && Input.GetButtonDown(button_string + "Click"))
            {
                Decatch();
            }
        }
        else
        {
            if (selectedObject_rb && Input.GetButton(button_string + "Click"))
            {
                Use();
            }
            else if (selectedObject_rb && Input.GetButtonUp(button_string + "Click"))
            {
                Deuse();
            }
        }

        //if (Input.GetButton(button_string + "Use"))
        //{
        //    Catch();
        //}
        //else if (Input.GetButtonUp(button_string + "Use"))
        //{
        //    Decatch();
        //}

        if (!catched && myVirtualHand.localPosition != new Vector3(0, 0, 8.5f))
        {
            myVirtualHand.localPosition = new Vector3(0, 0, 8.5f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        EnviromentObject eo = other.GetComponentInParent<EnviromentObject>();

        if (!eo || !eo.grabbable)
            return;

        Rigidbody othersRb = other.GetComponentInParent<Rigidbody>();

        if (catched || !othersRb)
            return;

        if (selectedObject_rb)
        {
            selectedObject_eo.Dehighlight();
        }

        selectedObject_rb = othersRb;

        selectedObject_eo.Highlight();
    }

    void OnTriggerExit(Collider other)
    {
        EnviromentObject eo = other.GetComponentInParent<EnviromentObject>();

        if (!eo || !eo.grabbable)
            return;

        Rigidbody othersRb = other.GetComponentInParent<Rigidbody>();

        if (catched || !othersRb)
            return;

        if (othersRb == selectedObject_rb)
        {
            selectedObject_eo.Dehighlight();
        }

        selectedObject_rb = null;
    }

    public void Catch()
    {
        if (!selectedObject_rb)
        {
            handJoint.gameObject.SetActive(false);
        }
        else if (!catched)
        {
            selectedObject_eo.Grab(this);
            catched = true;
            handJoint.gameObject.SetActive(true);
            handJoint.connectedBody = selectedObject_rb;
        }
    }

    public void Decatch()
    {
        catched = false;

        if (selectedObject_rb)
        {
            selectedObject_eo.Degrab();
        }

        selectedObject_rb = null;
        handJoint.connectedBody = null;
        handJoint.gameObject.SetActive(false);
    }

    public void Use()
    {
        selectedObject_eo.Use();
    }

    public void Deuse()
    {
        selectedObject_eo.Deuse();
    }
}