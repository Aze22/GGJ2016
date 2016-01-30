using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
	// Public variables:
	// - Triggers can only be switched on
	// - Switches can be chained, so that they can only be activated in a particular order
	// - - Chained switches may reset if activated in the wrong order
	public bool m_isTrigger = false;
	public Switch m_previousSwitch;
	public bool m_resetChain = false;

	// Private variables
	private int m_index = 0;
	private GameStateManager gameStateManager;

    public GameObject m_state1Mesh;
    public GameObject m_state2Mesh;

    public GameObject[] objectsToActivate;

	// Initialization
	void Start () {
		gameStateManager = FindObjectOfType<GameStateManager>() as GameStateManager;
	}

	// Function to check if this switch is a trigger
	public bool IsTrigger() {
		return m_isTrigger;
	}

	// Function to set the switch index
	// Called by GameStateManager
	public void SetIndex(int index) {
		m_index = index;
	}

	// Function to get the switch index
	// This is used when reseting a chain
	public int GetIndex() {
		return m_index;
	}

	// Call this function to set the switch state
	// This function should update the switch asset to an appropriate state (e.g. change graphic)
	public bool SetState(bool state) {
		bool success = false;
		Debug.Log("Setting switch state to " + state);

		// Modify this code to toggle the different states, based on the final switch assets
		if (state) {
			// Only activate the switch if it has no previous switch, or the previous switch is active
			if ((m_previousSwitch == null) || (gameStateManager.GetSwitchState(m_previousSwitch.GetIndex()))) {
				Debug.Log("Previous switch: " + m_previousSwitch);
				//gameObject.GetComponent<SpriteRenderer>().color = Color.red;
				success = true;
			} else if ((m_previousSwitch) && (m_resetChain)) {
				// Reset the chain
				ResetChain();
				success = false;
			}

            m_state1Mesh.gameObject.SetActive(false);
            m_state2Mesh.gameObject.SetActive(true);

        } else {
			// Only deactivate the switch if it is not a trigger, or we are resetting a chain
			if (!m_isTrigger) {
                //gameObject.GetComponent<SpriteRenderer>().color = Color.green;     
                success = true;
			}

			if ((m_previousSwitch) && (m_resetChain)) {
				//gameObject.GetComponent<SpriteRenderer>().color = Color.green;
				success = true;
				ResetChain();
			}

            m_state1Mesh.gameObject.SetActive(true);
            m_state2Mesh.gameObject.SetActive(false);
        }

        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            if (objectsToActivate[i] != null)
            {
                objectsToActivate[i].SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
            }
        }

		Debug.Log("Success: " + success);
		return success;
	}

	// Call this function when reseting to avoid any issues with chains
	public void ResetState() {
		//gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        m_state1Mesh.gameObject.SetActive(true);
        m_state2Mesh.gameObject.SetActive(false);
    }

	// Function to reset a switch chain
	public void ResetChain() {
        //gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        m_state1Mesh.gameObject.SetActive(true);
        m_state2Mesh.gameObject.SetActive(false);

        if (m_previousSwitch) {
			gameStateManager.BroadcastMessage("ResetChain", m_previousSwitch.GetIndex());
		}
	}
}