using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public Vector3 m_startPosition;
	public float m_movementSpeed = 5f;
    private CharacterController m_characterController;

	// Use this for initialization
	void Start ()
    {
        m_characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ApplyGravity();
        
	}

    private void ApplyGravity()
    {
        //m_characterController.Move(new vecto)
    }
}