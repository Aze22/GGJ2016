using UnityEngine;
using System.Collections;


public class NPCScript : MonoBehaviour
{
    // Public variables
    public static NPCScript Instance;
    public Vector3 m_startPosition;
	public float m_movementSpeed = 5f;
    public float m_gravityMultiplier = 1f;

    // Private variables
    private CharacterController m_npcController;
    private Vector3 m_finalMovement;
    private Vector3 m_cameraOffset;
    private Switch m_nearbySwitch;
    public DoorScript m_nearbyDoor;
    float value = 0.000f;

  

    // Constants

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
    	// Set up the character controller and movement
        m_npcController = GetComponent<CharacterController>();
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

	// Initializes movement
    public void EarlySetup()
    {
        m_finalMovement = Vector3.zero;
    }

    // Function to check movement inputs
    private void ProcessMovement()
    {
        float h, v;
        v = Mathf.Cos(value);
        value += 0.05f;
        h = Mathf.Sin(value);
     
       // float h = Input.GetAxis("Horizontal");
       // float v = Input.GetAxis("Vertical");

        m_finalMovement += new Vector3(v, 0, -h);
        m_finalMovement *= m_movementSpeed;
    }

    // Function to handle gravity
    private void ProcessGravity()
    {
        m_finalMovement += new Vector3(0, Physics.gravity.y * m_gravityMultiplier, 0);  
    }

    // Function to move the character
    public void ApplyFinalMovement()
    {
        m_npcController.Move(m_finalMovement * Time.deltaTime);
    }

    public void OnTriggerExit(Collider other)
    {
        DoorScript doorScript = other.transform.parent.GetComponent<DoorScript>();

        if (doorScript != null)
        {
            doorScript.Close();
            m_nearbyDoor = null;
        }

		if (m_nearbySwitch == other.transform.parent.GetComponent<Switch>()) {
			m_nearbySwitch = null;
		}
    }
}