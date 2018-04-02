using System;
using UnityEngine;

public class Player_Virtual_Controller : MonoBehaviour
{
    public Transform[] bodies;
    public Transform[] addbodies;
    public float move_speed;
    public float rotate_speed;

    [SerializeField]
    Torso torso;

    float current_move_speed;
    float current_rotate_speedd;

    bool isCrouching = false;
    bool doOnce = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            rotate_speed += 2.5f;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            rotate_speed -= 2.5f;
        }

        if (Input.GetButtonDown("Crouching"))
        {
            isCrouching = !isCrouching;

            doOnce = true;

        }

        if (doOnce)
        {
            if (isCrouching)
            {
                torso.transform.localPosition = new Vector3(torso.transform.localPosition.x, -1.25f, torso.transform.localPosition.z);
            }
            else
            {
                torso.transform.localPosition = new Vector3(torso.transform.localPosition.x, 0, torso.transform.localPosition.z);
            }

            doOnce = false;
        }

        if (torso.transform.localPosition.y < -0.5f)
        {
            isCrouching = true;
            torso.collider.height = 1.5f;
        }
        else
        {
            isCrouching = false;
            torso.collider.height = 2;
        }
    }

    private void FixedUpdate()
    {
        foreach (Transform body in addbodies)
        {
            body.localEulerAngles = new Vector3(
                body.localEulerAngles.x,
                body.localEulerAngles.y,
                0
                );

            if (body != addbodies[2] && body != addbodies[0] && body != addbodies[1])
            {
                body.RotateAround(addbodies[3].position, addbodies[3].up, rotate_speed * Input.GetAxis("Mouse X") * Time.fixedDeltaTime);
            }

            if (body != addbodies[3])
            {
                body.RotateAround(addbodies[3].position, addbodies[3].right, rotate_speed * -Input.GetAxis("Mouse Y") * Time.fixedDeltaTime);
            }

            if (body == addbodies[3])
            {
                body.GetComponent<Rigidbody>().MovePosition(body.GetComponent<Rigidbody>().position + Input.GetAxis("Vertical") * body.forward * move_speed * Time.fixedDeltaTime
                    + Input.GetAxis("Horizontal") * body.right * move_speed * Time.fixedDeltaTime);

                body.GetComponent<Rigidbody>().AddForce(move_speed * 400 * Vector3.up * (Input.GetKeyDown(KeyCode.Space) ? 1 : 0));
            }
        }
    }
}

[Serializable]
public class Torso
{
    public Transform transform;
    public Rigidbody rigidbody;
    public CapsuleCollider collider;
}