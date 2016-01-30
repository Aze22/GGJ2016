using UnityEngine;
using System.Collections;
using System;

public class DoorScript : MonoBehaviour
{
    private Animation m_animation;
    public bool m_openAtStart = false;
    private bool m_open = false;

    void Start()
    {
        m_animation = GetComponent<Animation>();

        if(m_openAtStart)
        {
            Open();
        }
    }

    public void Open()
    {
        if(!m_open)
        {
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
        }
    }
}
