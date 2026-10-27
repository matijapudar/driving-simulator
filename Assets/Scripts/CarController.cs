using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 100f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        float forward = 0f, turn = 0f;
        if (kb.upArrowKey.isPressed)    forward += 1f;
        if (kb.downArrowKey.isPressed)  forward -= 1f;
        if (kb.rightArrowKey.isPressed) turn += 1f;
        if (kb.leftArrowKey.isPressed)  turn -= 1f;

        rb.MovePosition(rb.position + transform.forward * forward * speed * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn * forward * turnSpeed * Time.fixedDeltaTime, 0f));
    }
}