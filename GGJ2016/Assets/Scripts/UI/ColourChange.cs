using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColourChange : MonoBehaviour {

    public float r, g, b;
    public float x, y, theta = 0.0f;
    public bool reverse;
    private Image m_image;
    private RectTransform rectTransform;

    // Count the number of ticks before the end of the day
    private int m_numberTicks = 0;
    public int total_ticks = 50 * (2 * 60);

    // Update rates based on TOTAL_TICKS
    private float dx;
    private float dcolor;

    void UpdateColor()
    {
        if (!reverse)
        {
			r -= dcolor;
			g -= dcolor;
			b += dcolor;
            m_image.color = new Color(r, g, b, 1f);
        }
        else
        {
			r += dcolor;
			g += dcolor;
			b -= dcolor;
            m_image.color = new Color(r, g, b, 1f);
        }
        if (r < 0f || r > 1f)
        {
            reverse = !reverse;
        }
    }
    void UpdatePosition()
    {
        theta += dx;
        x = ((float)Screen.width * 0.5f) + (35f * Mathf.Sin(theta));
        y = 70f + (35f * Mathf.Cos(theta));
        //rectTransform.position = new Vector3(x, Mathf.Atan(x / 2) + (x * x * x) / 10000, 0);
        rectTransform.position = new Vector3(x, y, 0f);
    }

    void Start()
    {
		dx = (2f * Mathf.PI) / (float)total_ticks;
		dcolor = (1.6666666667f) / (float)total_ticks;
        r = 1f;
        g = 1f;
        b = 0f;
        m_image = GetComponent<Image>();
        m_image.color = new Color(r, g, b, 1f);
        rectTransform = GetComponent<RectTransform>() as RectTransform;
        x = rectTransform.position.x;
        y = rectTransform.position.y;
       //rectTransform.position = new Vector3(x, Mathf.Atan(x / 2) + (x * x * x) / 50, 0);

    }
	
	// Update is called once per frame
	void Update ()
    {
		if (m_numberTicks < total_ticks) {
			m_numberTicks++;
        	UpdateColor();
        	UpdatePosition();        
        } else {
			GameStateManager.Instance.BroadcastMessage("Reset");
        }
	}
}
