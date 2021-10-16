using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBurst : MonoBehaviour
{
    public GameObject bursteffect;
    public GameObject burningEffect;
    public AudioSource explosionSound;
    public AudioSource fireSound;
    float duration = 4f;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "spikes")
        {
            explosionSound.Play();
            GameObject clone =  (GameObject)Instantiate(bursteffect, transform.position, transform.rotation);
            Destroy(clone, duration);
            Destroy(gameObject);
        }
        else if(collider.tag == "LavaStream")
        {
            fireSound.Play();
            GameObject clone = (GameObject)Instantiate(burningEffect, transform.position, transform.rotation);
            Destroy(clone, duration);
            Destroy(gameObject);
        }
    }
   
}
