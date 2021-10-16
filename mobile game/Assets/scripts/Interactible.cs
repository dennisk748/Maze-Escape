
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public float radius = 3f;

    public Transform interactionTransform;

    bool isFocus = false;
    bool hasInteracted = false;
    Transform player;

    public virtual void interact()
    {
        //this method is meant to overriten
        Debug.Log("ïnteracting with" + transform.name); 

    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                interact();
                Debug.Log("interact with");
                hasInteracted = true;
            }
        }
    }

    public void onFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
    public void onDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
