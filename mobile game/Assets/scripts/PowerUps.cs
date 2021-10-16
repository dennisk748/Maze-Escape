using System.Collections;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject pickUpEffect;
    public float multiplier = 1.1f;
    public float duration = 4f;
    float destroyEffectDuration = 2f;
    public AudioSource powerupPickup;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            powerupPickup.Play();
            //StartCoroutine(pickUp(other));
            pickUp(other);
        }
    }
   
    void pickUp(Collider player)
    {
        GameObject clone =  Instantiate(pickUpEffect, transform.position, transform.rotation);
        Destroy(clone, destroyEffectDuration);
      

        player.transform.localScale *= multiplier;
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats.currentHealth < 100) 
        {
            stats.currentHealth *= multiplier; 
        }

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        //yield return new WaitForSeconds(duration);

        //stats.currentHealth /= multiplier;


        Destroy(gameObject);
    }
}
