using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        { 
            DeathTimer();
            
        }
    }
    public void DeathTimer()
    {
        LevelManager.Instance.Death();
    }
}
