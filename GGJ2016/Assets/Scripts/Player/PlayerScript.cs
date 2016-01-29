using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public Vector3 m_startPosition;
	public float m_movementSpeed = 5f;
    public float m_gravityMultiplier = 1f;
    private CharacterController m_characterController;
    private Vector3 m_finalMovement;

	// Use this for initialization
	void Start ()
    {
        m_characterController = GetComponent<CharacterController>();
        m_finalMovement = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        EarlySetup();
        ProcessMovement();
        ProcessGravity();
        ApplyFinalMovement();
	}

    public void EarlySetup()
    {
        m_finalMovement = Vector3.zero;
    }

    private void ProcessMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        m_finalMovement += new Vector3(h, 0, v);

    }

    private void ProcessGravity()
    {
        m_finalMovement += new Vector3(0, Physics.gravity.y * m_gravityMultiplier, 0);
       
    }

    public void ApplyFinalMovement()
    {
        m_characterController.Move(m_finalMovement * m_movementSpeed * Time.deltaTime);
    }
}