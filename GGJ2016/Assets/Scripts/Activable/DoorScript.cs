using UnityEngine;
using System.Collections;
using System;

public class DoorScript : MonoBehaviour
{
    private Animation m_animation;
    public bool m_openAtStart = false;
    private bool m_open = false;
    public bool m_locked = true;
    public Light m_light;
    public Color m_lockedColor;
    public Color m_unlockedColor;

    public GameStateManager.KeyCards keyCardRequired = GameStateManager.KeyCards.None;

    void Start()
    {
        m_animation = GetComponent<Animation>();

        if(m_openAtStart)
        {
            Open();
        }

        if(m_locked || keyCardRequired != GameStateManager.KeyCards.None)
        {
            Lock();
        }
        else
        {
            UnLock();
        }
    }

    public void Lock()
    {
        m_locked = true;
        m_light.color = m_lockedColor;

        if ( PlayerScript.Instance.m_nearbyDoor != null)
            PlayerScript.Instance.m_nearbyDoor.Close();
    }

    public void UnLock()
    {
        m_locked = false;
        m_light.color = m_unlockedColor;

        if (PlayerScript.Instance.m_nearbyDoor != null)
            PlayerScript.Instance.m_nearbyDoor.Open();
    }

    public void Open()
    {
        if(!m_open && (!m_locked || GameStateManager.Instance.HasKeyCard(keyCardRequired)))
        {
            if(m_locked)
            {
                m_light.color = m_unlockedColor;
            }

            m_animation["DoorOpen"].speed = 1;
            m_animation.Play("DoorOpen");
            m_open = true;
        }
    }

    public void Close()
    {
        if (m_open)
        {
            m_animation["DoorOpen"].speed = -1;

            if(!m_animation.isPlaying)
                m_animation["DoorOpen"].time = m_animation["DoorOpen"].length;

            m_animation.Play("DoorOpen");
            m_open = false;

            if (m_locked)
            {
                m_light.color = m_lockedColor;
            }
        }
    }

    public void Activate()
    {
        if (keyCardRequired == GameStateManager.KeyCards.None && m_locked)
            UnLock();
        else
            Lock();
    }
}
