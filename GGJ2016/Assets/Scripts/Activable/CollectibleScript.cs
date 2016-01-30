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

    // Function called when the player enters this collectible's trigger - Handles pickup. It is overriden by child classes for specific behaviors.
	public virtual void Pickup()
    {
        if(destroyOnPickup)
            Destroy(gameObject);
    }
}
