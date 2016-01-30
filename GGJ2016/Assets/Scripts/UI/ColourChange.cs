using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColourChange : MonoBehaviour {

    public float colour, r, g, b;
    public float x, y;
    public bool reverse;
    private Image m_image;
    private RectTransform rectTransform;

    void UpdateColor()
    {
        if (!reverse)
        {
            r -= 1f / 150f;
            g -= 1f / 150f;
            b += 1f / 150f;
            m_image.color = new Color(r, g, b, 1);
        }
        else
        {
            r += 1f / 150f;
            g += 1f / 150f;
            b -= 1f / 150f;
            m_image.color = new Color(r, g, b, 1);
        }
        if (r < 0 || r > 1)
        {
            reverse = !reverse;
        }
    }
    void UpdatePosition()
    {
        x += 1f;
        //rectTransform.position = new Vector3(x, Mathf.Atan(x / 2) + (x * x * x) / 10000, 0);
        rectTransform.position = new Vector3(x,y, 0);
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
        UpdateColor();
        UpdatePosition();        
	}
}
