using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColourChange : MonoBehaviour {

    public float colour, r, g, b;
    public float x, y;
    public bool reverse;
    private Image m_image;
    private RectTransform rectTransform;

    // Count the number of ticks before the end of the day
    private int m_numberTicks = 0;
    private const int TOTAL_TICKS = 7500;

    // Update rates based on TOTAL_TICKS
    private const float DX = 375f / (float)TOTAL_TICKS;
    private const float DCOLOR = (1.6666666667f) / (float)TOTAL_TICKS;

    void UpdateColor()
    {
        if (!reverse)
        {
			r -= DCOLOR;
			g -= DCOLOR;
			b += DCOLOR;
            m_image.color = new Color(r, g, b, 1);
        }
        else
        {
			r += DCOLOR;
			g += DCOLOR;
			b -= DCOLOR;
            m_image.color = new Color(r, g, b, 1);
        }
        if (r < 0 || r > 1)
        {
            reverse = !reverse;
        }
    }
    void UpdatePosition()
    {
        x += DX;
        //rectTransform.position = new Vector3(x, Mathf.Atan(x / 2) + (x * x * x) / 10000, 0);
        rectTransform.position = new Vector3(x, y, 0);
    }

    void Start()
    {
        r = 1;
        g = 1;
        b = 0;
        m_image = GetComponent<Image>();
        m_image.color = new Color(r,g,b,1);
        rectTransform = GetComponent<RectTransform>() as RectTransform;
        x = rectTransform.position.x;
        y = rectTransform.position.y;
       //rectTransform.position = new Vector3(x, Mathf.Atan(x / 2) + (x * x * x) / 50, 0);

    }
	
	// Update is called once per frame
	void Update ()
    {
		if (m_numberTicks < TOTAL_TICKS) {
			m_numberTicks++;
        	UpdateColor();
        	UpdatePosition();        
        }
	}
}
