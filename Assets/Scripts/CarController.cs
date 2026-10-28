using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float maxSpeed = 20f;      // najveća brzina (m/s)
    public float acceleration = 8f;   // koliko brzo ubrzava
    public float brakeForce = 20f;    // koliko jako koči
    public float friction = 3f;       // usporavanje kad ništa ne pritiskaš
    public float turnSpeed = 100f;    // skretanje (stepeni u sekundi)

    Rigidbody rb;
    float currentSpeed;

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

        float dt = Time.fixedDeltaTime;

        if (forward > 0f)
            currentSpeed += acceleration * dt;
        else if (forward < 0f)
            currentSpeed -= (currentSpeed > 0f ? brakeForce : acceleration) * dt;
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, friction * dt);

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed * 0.4f, maxSpeed);

        Vector3 velocity = transform.forward * currentSpeed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;

        float steer = turn * turnSpeed * (currentSpeed / maxSpeed) * dt;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, steer, 0f));
    }
}