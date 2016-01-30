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

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastCheck.position, player.position + new Vector3(0,-2f,0), out hit, 2000, layer))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider != null && hit.collider.name.Contains("Wall"))
            {
                

                if (currentHiddenObject != null)
                    currentHiddenObject.sharedMaterial = normalMat;

                currentHiddenObject = hit.collider.GetComponent<Renderer>();

                if (currentHiddenObject != null)
                    currentHiddenObject.sharedMaterial = transparentMat;
            }
            else
            {
                if (currentHiddenObject != null)
                    currentHiddenObject.sharedMaterial = normalMat;
            }
        }
        else
        {
            if (currentHiddenObject != null)
                currentHiddenObject.sharedMaterial = normalMat;
        }
    }
}