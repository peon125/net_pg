using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller inst;

    public Player_Virtual_Controller pvc;

    [SerializeField]
    Hand[] hands;
    [SerializeField]
    Rigidbody[] bodies;

    float rotate_speed;
    Vector3 camera_offset;

    void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        Cursor.visible = false;
        rotate_speed = pvc.rotate_speed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.visible = !Cursor.visible;
        }

        Cursor.lockState = !Cursor.visible ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void FixedUpdate()
    {
        for (int i = 1; i < bodies.Length - 1; i++)
        {
            bodies[i].MovePosition(pvc.bodies[i].position);
            bodies[i].MoveRotation(pvc.bodies[i].rotation);
        }
    }

    private void LateUpdate()
    {
        bodies[bodies.Length - 1].MovePosition(pvc.bodies[bodies.Length - 1].position);
        bodies[bodies.Length - 1].MoveRotation(pvc.bodies[bodies.Length - 1].rotation);
    }
}