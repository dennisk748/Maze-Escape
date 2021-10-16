using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObject : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    

    private void OnCollisionEnter(Collision collision)
    {
 
        
            Destroy(target);
        
        Destroy(gameObject);
    }
    
}
