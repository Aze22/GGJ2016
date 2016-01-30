using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusText : MonoBehaviour {
	Text m_text;

	// Status gradually fades
	const float FADE_DURATION = 2f;
	float m_fadeTimeLeft;

	// Use this for initialization
	void Start () {
		m_text = gameObject.GetComponent<Text>();
		SetStatus("Escape before time runs out");
	}
	
	// Update is called once per frame
	void Update () {
		m_fadeTimeLeft -= Time.deltaTime;

		if (m_fadeTimeLeft < 0f) {
			//m_text.text = "";
		} else {
			Color color = m_text.material.color;
			color.a = 1f * (1 - ((FADE_DURATION - m_fadeTimeLeft) / FADE_DURATION));
			m_text.material.color = color;
		}
	}

	// Function to set a status messsage
	// Call this function using statusText.BroadcastMessage("SetStatus", status)
	public void SetStatus(string status) {
		Debug.Log("Show status: " + status);
        if (m_text != null)
        {
            Color color = m_text.material.color;
            color.a = 1f;
            m_text.material.color = color;
            m_text.text = status;
            m_fadeTimeLeft = FADE_DURATION;
        }
	}
}