using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Fade : MonoBehaviour {

    public Image m_image;
    float opacity = 1f;

	// Use this for initialization
	void Start ()
    {
        m_image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_image.enabled)
        {
            opacity -= 0.03f;
            m_image.color = new Color(1, 1, 1, opacity);
        }
        if (opacity < 0f)
        {
            m_image.enabled = false;
        }
	
	}
}
