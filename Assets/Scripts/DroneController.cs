using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroneController : MonoBehaviour
{
    [SerializeField] private float moveForce = 50f;
    [SerializeField] private float verticalForce = 30f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float rotationSpeed = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.drag = 0f;
        rb.angularDrag = 0.5f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        RotateWithMouse();
    }

    void FixedUpdate()
    {
        MoveDrone();
    }

    void MoveDrone()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");     // W/S

        Vector3 moveInput = transform.forward * vertical + transform.right * horizontal;

        // Aplicamos fuerza horizontal solo si no superamos la velocidad máxima
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (horizontalVelocity.magnitude < maxSpeed || Vector3.Dot(horizontalVelocity, moveInput) < 0)
        {
            rb.AddForce(moveInput.normalized * moveForce, ForceMode.Force);
        }

        // Fuerza vertical
        float yInput = 0f;
        if (Input.GetKey(KeyCode.Space)) yInput = 1f;
        else if (Input.GetKey(KeyCode.LeftControl)) yInput = -1f;

        Vector3 verticalForceVector = Vector3.up * yInput * verticalForce;

        // Aplicamos fuerza vertical solo si no superamos la velocidad máxima vertical
        if (Mathf.Abs(rb.velocity.y) < maxSpeed || Mathf.Sign(rb.velocity.y) != Mathf.Sign(yInput))
        {
            rb.AddForce(verticalForceVector, ForceMode.Force);
        }
    }

    void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.Rotate(Vector3.up * mouseX, Space.World);
        transform.Rotate(Vector3.right * mouseY, Space.Self);
    }
}