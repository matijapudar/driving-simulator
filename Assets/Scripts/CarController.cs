using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float acceleration = 8f;
    public float brakeForce = 20f;
    public float friction = 3f;
    public float turnSpeed = 100f;
    public float grip = 8f;          // koliko auto drži pravac (veće = manje klizi)
    public float driftGrip = 1.5f;   // prianjanje dok držiš Space

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
        bool drifting = kb.spaceKey.isPressed;

        float dt = Time.fixedDeltaTime;

        if (forward > 0f)
            currentSpeed += acceleration * dt;
        else if (forward < 0f)
            currentSpeed -= (currentSpeed > 0f ? brakeForce : acceleration) * dt;
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, friction * dt);

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed * 0.4f, maxSpeed);

        Vector3 desired = transform.forward * currentSpeed;
        Vector3 velocity = rb.linearVelocity;
        float fall = velocity.y;
        velocity.y = 0f;
        velocity = Vector3.Lerp(velocity, desired, (drifting ? driftGrip : grip) * dt);
        velocity.y = fall;
        rb.linearVelocity = velocity;

        float steer = turn * turnSpeed * (currentSpeed / maxSpeed) * dt;
        if (drifting) steer *= 1.5f;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, steer, 0f));
    }
}