using UnityEngine;
using System.Collections;
using System;

public class DoorScript : MonoBehaviour
{
    private Animation m_animation;
	private MeshRenderer m_meshRenderer;
    public bool m_openAtStart = false;
    private bool m_open = false;
    public bool m_locked = true;
    public Light m_light;
    public Color m_lockedColor;
    public Color m_unlockedColor;
    public AudioClip openSound;
	public AudioClip closeSound;
	public GameObject musicManager;
	private AudioSource audioSource;
	public GameStateManager.KeyCards keyCardRequired = GameStateManager.KeyCards.None;

	// Materials for standard and keycard doors
	public Material openMaterial;
	private Material closedMaterial;
	public Material[] keycardMaterials = new Material[(int)GameStateManager.KeyCards.NumKeyCards - 1];

    void Start()
    {
        m_animation = GetComponent<Animation>();
        m_meshRenderer = GetComponentInChildren<MeshRenderer>();
        closedMaterial = m_meshRenderer.material;

        if (m_openAtStart)
        {
            Open();
        }

        if ((m_locked) || (keyCardRequired != GameStateManager.KeyCards.None))
        {
            Lock();
        }
        else
        {
            UnLock();
        }

        if (keyCardRequired != GameStateManager.KeyCards.None) {
			m_meshRenderer.material = keycardMaterials[GameStateManager.perms[(int)keyCardRequired - 1]];
        }

		GameObject go = GameObject.Instantiate(musicManager, transform.position, Quaternion.identity) as GameObject;
		audioSource = go.GetComponentInParent<AudioSource>() as AudioSource;
		audioSource.transform.position = transform.position;
    }

    public void Lock()
    {
        m_locked = true;
        m_light.color = m_lockedColor;

        if (keyCardRequired == GameStateManager.KeyCards.None) {
			m_meshRenderer.material = closedMaterial;
		}

		if (PlayerScript.Instance.m_nearbyDoor != null) {
			PlayerScript.Instance.m_nearbyDoor.Close();
		}
    }

    public void UnLock()
    {
        m_locked = false;
        m_light.color = m_unlockedColor;

		if (keyCardRequired == GameStateManager.KeyCards.None) {
			m_meshRenderer.material = openMaterial;
		}

		if (PlayerScript.Instance.m_nearbyDoor != null) {
			PlayerScript.Instance.m_nearbyDoor.Open();
		}
    }

    public void Open()
    {
        if ((!m_open) && ((!m_locked) || (GameStateManager.Instance.HasKeyCard(keyCardRequired))))
        {
            if(m_locked)
            {
                m_light.color = m_unlockedColor;
            }

            m_animation["DoorOpen"].speed = 1;
            m_animation.Play("DoorOpen");
            m_open = true;

            if (audioSource) {
				audioSource.clip = openSound;
            	audioSource.Play();
            }
        }
    }

    public void Close()
    {
        if (m_open)
        {
			m_animation["DoorOpen"].speed = -1;

			if (!m_animation.isPlaying) {
                m_animation["DoorOpen"].time = m_animation["DoorOpen"].length;
			}

            m_animation.Play("DoorOpen");
            m_open = false;

            if (audioSource) {
				audioSource.clip = openSound;
            	audioSource.Play();
            }

            if (m_locked)
            {
                m_light.color = m_lockedColor;
            }
        }
    }

    public void Activate()
    {
        if (keyCardRequired == GameStateManager.KeyCards.None && m_locked) {
            UnLock();
        } else {
            Lock();
		}
    }
}
