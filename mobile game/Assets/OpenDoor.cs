using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
     public Transform doorObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (doorObject != null)
            Destroy (doorObject.gameObject);
    }
}
