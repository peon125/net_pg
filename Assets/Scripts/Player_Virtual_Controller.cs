using UnityEngine;

public class Player_Virtual_Controller : MonoBehaviour
{
    public Transform[] bodies;
    public Transform[] addbodies;
    public float move_speed;
    public float rotate_speed;

    float current_move_speed;
    float current_rotate_speedd;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            rotate_speed += 2.5f;
        if (Input.GetKeyDown(KeyCode.I))
            rotate_speed -= 2.5f;
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
            }
        }
    }
}