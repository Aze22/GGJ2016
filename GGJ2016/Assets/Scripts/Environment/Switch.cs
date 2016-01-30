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
	protected int GetIndex() {
		return m_index;
	}

	// Call this function to set the switch state
	// This function should update the switch asset to an appropriate state (e.g. change graphic)
	public void SetState(bool state) {
		Debug.Log("Setting switch state to " + state);

		// Modify this code to toggle the different states, based on the final switch assets
		if (state) {
			// Only activate the switch if it has no previous switch, or the previous switch is active
			if ((m_previousSwitch == null) || (gameStateManager.GetSwitch(m_index))) {
				//gameObject.GetComponent<SpriteRenderer>().color = Color.red;
			} else if ((m_previousSwitch) && (m_resetChain)) {
				// Reset the chain
				ResetChain();
			}
		} else {
			// Only deactivate the switch if it is not a trigger, or we are resetting a chain
			if (!m_isTrigger) {
				//gameObject.GetComponent<SpriteRenderer>().color = Color.green;
			} else if ((m_previousSwitch) && (m_resetChain)) {
				//gameObject.GetComponent<SpriteRenderer>().color = Color.green;
				ResetChain();
			}
		}
	}

	// Function to reset a switch chain
	public void ResetChain() {
		//gameObject.GetComponent<SpriteRenderer>().color = Color.green;

		if (m_previousSwitch) {
			gameStateManager.BroadcastMessage("ResetChain", m_previousSwitch.GetIndex());
		}
	}
}