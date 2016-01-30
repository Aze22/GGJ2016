using UnityEngine;
using System.Collections;

public class CollectibleScript : MonoBehaviour
{
    // Collectible type
    public enum Type
    {
        KEYCARD
    }

    // Collectible type in inspector
    public Type m_type = Type.KEYCARD;

    public bool destroyOnPickup = true;
    private Animation m_animation;

    public virtual void Start()
    {
        m_animation = GetComponent<Animation>();
    }

    // Function called when the player enters this collectible's trigger - Handles pickup. It is overriden by child classes for specific behaviors.
	public virtual void Pickup()
    {
        if (destroyOnPickup)
        {
            if(m_animation != null)
                m_animation.Play("PickupAnimation");

            Destroy(gameObject, m_animation["PickupAnimation"].length);
        }
    }
}
