using UnityEngine;
using System.Collections;

public class BottomLight : MonoBehaviour 
{
	public float flashSpeed = 0.1f;
	public float flashAmplitude = 6f;

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
		value += flashSpeed;
		m_light.intensity = 5f + ((flashAmplitude * Mathf.Sin(value)) / 2f);
	}
}
