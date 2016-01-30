using UnityEngine;
using System.Collections;

public class KeycardCollectibleScript : CollectibleScript
{
    public GameStateManager.KeyCards m_keyType;

	public override void Pickup()
    {
        GameStateManager.Instance.CollectKeyCard(m_keyType);
        base.Pickup();
    }
}
