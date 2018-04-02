using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdzDoGory : EnviromentObject
{
    [SerializeField]
    float speed;

    Rigidbody rb;

    private void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody>();
    }

    public override void Grab(Hand hand)
    {
        handIAmGrabbedBy = hand;
        isGrabbed = true;

        StartCoroutine(RuszajSie());
    }

    public override void Degrab()
    {
        Dehighlight();

        isGrabbed = false;

        handIAmGrabbedBy = null;
    }

    IEnumerator RuszajSie()
    {
        while(true)
        {
            rb.MovePosition(transform.position + Vector3.up * speed * Time.deltaTime);
            Player_Controller.inst.pvc.addbodies[3].GetComponent<Rigidbody>().MovePosition(Player_Controller.inst.pvc.addbodies[3].position + Vector3.up * speed * Time.deltaTime);

            yield return 0;
        }
    }
}
