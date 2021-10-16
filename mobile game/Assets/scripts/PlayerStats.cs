using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public float HealthDecrease = 0.01f;

    public HealthBar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth =(int) maxHealth;
        healthbar.maxHealth((int)maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine( TakeDamage(HealthDecrease));
    }

    IEnumerator TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            yield return new WaitForSeconds(2f);
            currentHealth -= damage;

            healthbar.setHealth(currentHealth);
        }
        else
        {
            LevelManager.Instance.Death();
        }
    }
}
