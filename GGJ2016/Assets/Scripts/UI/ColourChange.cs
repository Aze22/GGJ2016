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
		float xOffset = -227.5f + ((theta * 455f) / (2f * Mathf.PI));
        x = ((float)Screen.width * 0.5f) + xOffset;
        //y = 70f + (35f * Mathf.Sin(theta));
		y = 70f - 22.5f * (Mathf.Atan(xOffset / 35f) - ((xOffset * xOffset * xOffset) / 10000000f));
		rectTransform.position = new Vector3(x, y, 0);
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
