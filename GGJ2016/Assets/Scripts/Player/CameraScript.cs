using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Material transparentMat;
    public Material normalMat;

    public Transform player;
    public LayerMask layer;

    public Transform raycastCheck;

    private Renderer currentHiddenObject = null;
    float timeVisible = 0f;

    void Update()
    {
        if (player != null && raycastCheck != null)
        {
            RaycastHit m_hit;
            Ray ray = new Ray(player.transform.position/* + new Vector3(0, -1, 0)*/, raycastCheck.position);

            Debug.DrawLine(raycastCheck.position, player.transform.position /*+ new Vector3(0,-1,0)*/);

            if (Physics.Raycast(ray, out m_hit, 3f, layer))
            {
                Debug.Log(m_hit.collider.name);
                if (m_hit.transform.name.Contains("Wall"))
                {
                    if (currentHiddenObject != null)
                        currentHiddenObject.sharedMaterial = normalMat;

                    currentHiddenObject = m_hit.collider.GetComponent<Renderer>();

                    if (currentHiddenObject != null)
                        currentHiddenObject.sharedMaterial = transparentMat;

                    timeVisible = 0f;
                }
                else
                {
                    timeVisible += Time.deltaTime;

                    if (timeVisible > 0.2f)
                    {
                        if (currentHiddenObject != null)
                            currentHiddenObject.sharedMaterial = normalMat;
                    }
                }
            }
            else
            {
                timeVisible += Time.deltaTime;

                if (timeVisible > 0.2f)
                {
                    if (currentHiddenObject != null)
                        currentHiddenObject.sharedMaterial = normalMat;
                }
            }
        }
    }
}