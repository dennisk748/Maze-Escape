using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsableWall : MonoBehaviour
{
    public float forceRequired = 10.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > forceRequired)
        {
            Destroy (gameObject);
        }
    }
}
