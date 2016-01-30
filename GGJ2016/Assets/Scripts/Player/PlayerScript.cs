using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    // Public variables
    public static PlayerScript Instance;
    public Vector3 m_startPosition;
	public float m_movementSpeed = 5f;
    public float m_gravityMultiplier = 1f;

    // Private variables
    private CharacterController m_characterController;
    private Camera m_camera;
    private Vector3 m_finalMovement;
    private Vector3 m_cameraOffset;
    private Switch m_nearbySwitch;
    public DoorScript m_nearbyDoor;

    // Constants
    private const float CAMERA_BOX_X = 2f;
	private const float CAMERA_BOX_Y = 2f;
	private const float CAMERA_BOX_Z = 2f;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
    	// Set up the character controller and movement
        m_characterController = GetComponent<CharacterController>();
        m_finalMovement = Vector3.zero;

        // Set up the camera and offset
        m_camera = FindObjectOfType(typeof(Camera)) as Camera;
        m_cameraOffset = m_camera.transform.position - transform.position;
	}

	// Update is called once per frame
	void Update ()
    {
        EarlySetup();
        ProcessKeyPresses();
        ProcessMovement();
        ProcessGravity();
        ApplyFinalMovement();
        UpdateCamera();
	}

	// Initializes movement
    public void EarlySetup()
    {
        m_finalMovement = Vector3.zero;
    }

    // Function to process key presses
    private void ProcessKeyPresses() {
    	// Handle the interact button
    	if (Input.GetButtonUp("Interact")) {
    		if (m_nearbySwitch) {
				GameStateManager.Instance.BroadcastMessage("ToggleSwitch", m_nearbySwitch.GetIndex());
    		}
    	}
    }

    // Function to check movement inputs
    private void ProcessMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

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
        m_characterController.Move(m_finalMovement * Time.deltaTime);

        if (transform.position.y < 3.5f) ;
    }

    // Function to update the camera position, based on character position
    private void UpdateCamera() {
    	Vector3 centreOffset = (m_camera.transform.position - transform.position) - m_cameraOffset;
    	Vector3 cameraDiff = Vector3.zero;

		if (centreOffset.x > CAMERA_BOX_X) {
			cameraDiff.x = centreOffset.x - CAMERA_BOX_X;
		} else if (centreOffset.x < -CAMERA_BOX_X) {
			cameraDiff.x = centreOffset.x + CAMERA_BOX_X;
		}

		if (centreOffset.y > CAMERA_BOX_Y) {
			cameraDiff.y = centreOffset.y - CAMERA_BOX_Y;
		} else if (centreOffset.y < -CAMERA_BOX_Y) {
			cameraDiff.y = centreOffset.y + CAMERA_BOX_Y;
		}

		if (centreOffset.z > CAMERA_BOX_Z) {
			cameraDiff.z = centreOffset.z - CAMERA_BOX_Z;
		} else if (centreOffset.z < -CAMERA_BOX_Z) {
			cameraDiff.z = centreOffset.z + CAMERA_BOX_Z;
		}

		m_camera.transform.position -= cameraDiff;
    }

    public void OnTriggerEnter(Collider other)
    {
        CollectibleScript collectibleScript = other.GetComponent<CollectibleScript>();

        if(collectibleScript != null)
        {
            collectibleScript.Pickup();
        }

        DoorScript doorScript = other.transform.parent.GetComponent<DoorScript>();

        if (doorScript!= null)
        {
            m_nearbyDoor = doorScript;
            doorScript.Open();
        }

        m_nearbySwitch = other.transform.parent.GetComponent<Switch>() as Switch;

        if(other.name.Contains("Killzone"))
        {
            GameStateManager.Instance.Reset();
        }
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