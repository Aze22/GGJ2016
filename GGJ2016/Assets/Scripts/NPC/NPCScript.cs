using UnityEngine;
using System.Collections;


public class NPCScript : MonoBehaviour
{
    // Public variables
    public Vector3 m_startPosition;
	public float m_movementSpeed = 5f;
    public float m_gravityMultiplier = 1f;
	public enum NpcBehaviour : int {
    	Static,
    	Linear,
    	Reciprocal,
    	Circular,
    	Follow,
  //  	Copy,
  //  	Mirror,
  //  	Path
    };
	public NpcBehaviour behaviour; 

    // Private variables
    private CharacterController m_npcController;
    private Vector3 m_finalMovement;
    private Vector3 m_cameraOffset;
    float value = 0.000f;
	Transform myTransform;
	float h = 0, v = 0;
	public int z = 5;


    // Constants

    // Use this for initialization
	void Start ()
	{
    	// Set up the character controller and movement
        m_npcController = GetComponentInParent<CharacterController>();
        m_finalMovement = Vector3.zero;
		behaviour = NpcBehaviour.Linear       ;
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

	void Awake()
	{
		myTransform = transform;
	}

    // Function to check movement inputs
    private void ProcessMovement()
	{
		//Static, Linear, Reciprocal, Circular, Follow, 
		//To Do Copy, Mirror, Path
		if (behaviour == NpcBehaviour.Static) {
			z = 1;
		}
		if (behaviour == NpcBehaviour.Linear) {
			z = 2;
		}
		if (behaviour == NpcBehaviour.Reciprocal) {
			z = 3;
		}
		if (behaviour == NpcBehaviour.Circular) {
			z = 4;
		}
		if (behaviour == NpcBehaviour.Follow) {
			z = 5;
		}

		switch (z) {
		case 1:
			// set everything to stop but this doesn't work 
			//myTransform.position = new Vector3(1.0f, 1.0f, 1.0f);
			m_finalMovement += new Vector3 (0, 0, 0);
			break; 
		case 2:
			//going straight on the horizontal
			h += 0.1f;
			m_finalMovement += new Vector3 (0, 0, h);
			break;
		case 3:
			//going straight on the vertical
			h += 0.1f;
			m_finalMovement += new Vector3 (0, 0, -h);
			break;
		case 4:
			value += 5;
			v = Mathf.Cos (value);
			h = Mathf.Sin (value);
			m_finalMovement += new Vector3 (v, 0, h);
			break;
		case 5:
			Transform target;
			target = GameObject.FindWithTag ("Player").transform;

			myTransform.rotation = Quaternion.Slerp (myTransform.rotation,
				Quaternion.LookRotation (target.position - myTransform.position), 3f * Time.deltaTime);
			myTransform.position += myTransform.forward * m_movementSpeed * Time.deltaTime;




			break;
		default:
			// hello
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