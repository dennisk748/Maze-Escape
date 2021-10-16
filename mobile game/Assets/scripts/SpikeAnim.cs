
using UnityEngine;

public class SpikeAnim : MonoBehaviour
{
    public Transform player;
    public Animator anim;
    public float radius = 7f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= radius)
            {
                anim.Play("spikeEmergence");
            }
        }
    }
}
