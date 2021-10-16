using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    private Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        rotation = new Vector3(0, 0, 50);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
