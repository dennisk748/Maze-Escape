using System.Collections;
using UnityEngine;


public class CameraMotorFunctions : MonoBehaviour
{
    private const float TIME_BEFORE_PLAY = 2.0f;
    public Transform lookAt;
    private Vector3 offset;
    private float smoothSpeed = 7.5f;
    private float distance = 5.0f;
    private float yOffset = 2.5f;
    private Vector3 desiredPosition;
    private Vector2 touchPosition;
    private float swipeResistance = 200.0f;
    private float startTime;

    public void Start()
    {
        startTime = Time.time;
        offset = new Vector3 (0, yOffset, -1f * distance);
    }
     void Update ()
    {
        if(Time.time - startTime < TIME_BEFORE_PLAY)
        return;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            slideCamera(true);
        else if(Input.GetKeyDown(KeyCode.RightArrow))
            slideCamera(true);
        
        if (Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown (1))
        {
            touchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp (0) || Input.GetMouseButtonUp (1))
        {
            float swipeForce = touchPosition.x - Input.mousePosition.x;
            if (Mathf.Abs (swipeForce) > swipeResistance)
            {
                if (swipeForce < 0)
                    slideCamera (true);
                else 
                    slideCamera (false);
            }
        }
    }
    private void FixedUpdate()
    {
         if(Time.time - startTime < TIME_BEFORE_PLAY)
        return;
        if (lookAt != null)
        {
            desiredPosition = lookAt.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.LookAt(lookAt.position + Vector3.up);
        }
    }
    public void slideCamera(bool left)
    {
        if (left)
            offset = Quaternion.Euler (0, 90 , 0) * offset;
        else
        {
            offset = Quaternion.Euler(0, -90, 0) * offset; 
        }
    }
}
