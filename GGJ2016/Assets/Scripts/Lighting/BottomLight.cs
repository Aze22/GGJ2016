using UnityEngine;
using System.Collections;

public class BottomLight : MonoBehaviour 
{

    private Light m_light;
    float value = 0f;

	// Use this for initialization
	void Start ()
    {
        m_light = GetComponent<Light>();
        m_light.intensity = 5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        value += 0.1f;
        m_light.intensity = 5f + 3 * Mathf.Sin(value);
	}
}
