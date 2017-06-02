using UnityEngine;
using System.Collections;


public class NPCScript : MonoBehaviour
{
    // Public variables
	public float m_movementSpeed = 5f;
	public float m_rotationSpeed = 3f;
    public float m_gravityMultiplier = 1f;
	public enum NpcBehaviour : int {
		Static,
    	Linear,
		Reciprocal,
      	Circular,
      	Follow,
   //  	Copy,
   //  	Mirror,
   //  	Path,
   		NumBehaviours
      };
 	public NpcBehaviour m_behaviour; 
  
    // Private variables
    private CharacterController m_npcController;
    private Vector3 m_finalMovement;
    private Vector3 m_cameraOffset;
    float value = 0f;
 	Transform myTransform;
  
    // Use this for initialization
	void Awake()
 	{
 		myTransform = transform;
 	}
 
	void Start ()
 	{
      	// Set up the character controller and movement
        m_npcController = GetComponentInParent<CharacterController>();
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
 		// Update is called once per frame
		float h = 0, v = 0;
 		value += 0.05f;

		// Handl various behaviours
		switch (m_behaviour) {
			case NpcBehaviour.Static:
				// set everything to stop but this doesn't work 
				//myTransform.position = new Vector3(1.0f, 1.0f, 1.0f);
				m_finalMovement += new Vector3 (0, 0, 0);
				break;

			case NpcBehaviour.Linear:
				//going straight on the horizontal
				h += m_movementSpeed;
				m_finalMovement += new Vector3 (0, 0, h);
				break;

			case NpcBehaviour.Reciprocal:
				//going straight on the vertical
				h += m_movementSpeed;
				m_finalMovement += new Vector3 (0, 0, -h);
				break;

			case NpcBehaviour.Circular:
				v = m_movementSpeed * Mathf.Cos (value);
				h = m_movementSpeed * Mathf.Sin (value);
	 			m_finalMovement += new Vector3 (v, 0, h);
 				break;

			case NpcBehaviour.Follow:
 				Transform target;
 				target = GameObject.FindWithTag ("Player").transform;
 				myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position - myTransform.position), m_rotationSpeed * Time.deltaTime);
	 			myTransform.position += myTransform.forward * m_movementSpeed * Time.deltaTime;
 				break;

 			default:
				Debug.Log("Unhandled NPC behaviour: " + m_behaviour);
 				break;
 		}

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
}