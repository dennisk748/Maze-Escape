using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookCamera : MonoBehaviour
{
    public VirtualJoystick camJoystick;
    public Transform LookAt;

    public float distance = 10.0f;
    public float currentX = 0.0f;
    public float currentY = 0.0f;
    public float sensitivityX = 3.0f;
    public float sensitivityY = 1.0f;
    void Update()
    {
        currentX += camJoystick.InputDirection.x * sensitivityX;
        currentY += camJoystick.InputDirection.z * sensitivityY;

    }
    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, -currentX, 0);
        transform.position = LookAt.position + rotation * dir;
        transform.LookAt(LookAt);
    }
}
