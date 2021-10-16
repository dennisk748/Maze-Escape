using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorFunctions : MonoBehaviour
{
    private const float TIME_BEFORE_PLAY = 2.0f;
    public float moveSpeed = 5.0f;
    public float drag  = 0.5f;
    public float terminalRotationSpeed = 25.0f;

    public VirtualJoystick moveJoystick;

    private Rigidbody controller;
    private Transform camTransform;

    public float boostSpeed = 5.0f;
    public float boostCooldown = 2.0f;
    private float lastBoost;
    private float startTime;

    public void Start()
    {
        lastBoost = Time.time - boostCooldown;
        controller = GetComponent<Rigidbody> ();
        controller.maxAngularVelocity = terminalRotationSpeed;
        controller.drag = drag;
        startTime = Time.time;
        camTransform = Camera.main.transform;
    }
    public void Update()
    {
        if(Time.time - startTime < TIME_BEFORE_PLAY)
        return;
        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        if (moveJoystick.InputDirection != Vector3.zero)
        {
            dir = moveJoystick.InputDirection;
        }

        if(dir.magnitude > 1)
        dir.Normalize();
        //rotate ourdirection vector with camera
        Vector3 rotatedDir = camTransform.TransformDirection(dir);
        rotatedDir = new Vector3(rotatedDir.x,0,rotatedDir.z);
        rotatedDir = rotatedDir.normalized * dir.magnitude;

        controller.AddForce(rotatedDir * moveSpeed);
    }
    public void Boost()
    {
        if(Time.time - startTime < TIME_BEFORE_PLAY)
        return;
        if (Time.time - lastBoost > boostCooldown)
        {
            lastBoost = Time.time;
        controller.AddForce(controller.velocity.normalized * boostSpeed, ForceMode.VelocityChange);
        }
    }
}
