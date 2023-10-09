using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField] private float movAcel = 3f;
    [SerializeField] private float maxSpd = 2f;
    [SerializeField] private float neckHSpd = 3f;
    [SerializeField] private float neckVSpd = 3f;
    [SerializeField] private float neckLen = 0.532f;
    private Vector2 neck = new Vector2(0, 0);
    private Transform parentTransform;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        parentTransform = transform.parent.transform;
        rb = transform.parent.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {Move(Vector3.forward);}
        if (Input.GetKey(KeyCode.A)) {Move(Vector3.left);}
        if (Input.GetKey(KeyCode.S)) {Move(Vector3.back);}
        if (Input.GetKey(KeyCode.D)) {Move(Vector3.right);}

        Rotate(new Vector2(-neckVSpd * Input.GetAxis("Mouse Y"), neckHSpd * Input.GetAxis("Mouse X")));
    }

    void Rotate(Vector2 r)
    {
        neck += r;
        transform.eulerAngles = new Vector3(neck.x, neck.y, 0);

        float ang = Mathf.Deg2Rad * (neck.y + 0);

        transform.position = new Vector3(
            parentTransform.position.x + neckLen * Mathf.Sin(ang),
            transform.position.y,
            parentTransform.position.z + neckLen * Mathf.Cos(ang)
        );

    }

    void Move(Vector3 d)
    {
        if (rb.velocity.magnitude < maxSpd)
        {
            d = Vector3.Normalize(Quaternion.Euler(0, neck.y, 0) * d); // adjusting movement angle to camera
            rb.velocity += d * movAcel / 100f;
        }
    }
}
