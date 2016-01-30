using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    public static GameStateManager Instance;
    public StatusText statusManager;

	// Types of key card
	public enum KeyCards : int {
        None,
        Red,
		Green,
		Blue,
		Magenta,
		Yellow,
		Cyan,
		NumKeyCards
	};
	private readonly string[] KEY_CARD_NAMES = {
        "None",
        "Red",
		"Green",
		"Blue",
		"Magenta",
		"Yellow",
		"Cyan"
	};

	// Whether each key card has been collected
	private bool[] m_keyCards = new bool[(int)KeyCards.NumKeyCards];

	// Array of switches, so that we can reset them
	private Switch[] m_switches;

	// Whether each switch has been activated
	private bool[] m_switchActive;

	// Controls whether debug keys for state management are enabled
	private const bool DEBUG_KEYS = true;

	// Use this for initialization
	void Start () {
        // Assign static instance for ease of access
        Instance = this;

		// Set up references to switches
		m_switches = GameObject.FindObjectsOfType(typeof(Switch)) as Switch[];
		m_switchActive = new bool[m_switches.Length];
		Debug.Log(m_switches.Length + " switches");

		// Reset the game state
		Reset();
	}

	// Function to reset the game state
	// Call this function using gameStateManager.BroadcastMessage("Reset")
	void Reset () {
		int index;

		// Reset the key cards
		for (index = 0; index < m_keyCards.Length; index++) {
			m_keyCards[index] = false;
		}

		// Reset the switch states
		for (index = 0; index < m_switches.Length; index++) {
			m_switches[index].SetIndex(index + 1);
			m_switches[index].ResetState();
			m_switchActive[index] = false;
		}
	}

	// Function to collect key cards
	// Call this function using gameStateManager.BroadcastMessage("CollectKeyCard", GameStateManager.KeyCards.colour)
	public void CollectKeyCard(KeyCards index) {
		Debug.Log("Collected key card " + index);
		statusManager.SetStatus(KEY_CARD_NAMES[(int)index] + " key card collected");
		m_keyCards[(int)index] = true;
	}

	// Public function to check whether a particular keycard has been collected
	public bool HasKeyCard(KeyCards index) {
		return m_keyCards[(int)index];
	}

	// Function to toggle switches
	// Call this function using gameStateManager.BroadcastMessage("ToggleSwitch", index)
	// Use 1-based counting
	void ToggleSwitch(int index) {
		// Switch is responsible for checking whether a switch can be toggled
		bool success = m_switches[index - 1].SetState(!m_switchActive[index - 1]);

		// Toggle the switch if we can
		if (success) {
			m_switchActive[index - 1] = !m_switchActive[index - 1];
			Debug.Log("Switch " + index + " state changed to " + m_switchActive[index - 1]);
		}
	}

	// Public function to get particular switch's state
	// Use 1-based counting
	public bool GetSwitchState(int index) {
		return m_switchActive[index - 1];
	}
	
	// Function to reset a switch chain
	// Call this function using gameStateManager.BroadcastMessage("ResetChain", index)
	// Use 1-based counting
	void ResetChain(int index) {
		m_switchActive[index - 1] = false;
		Debug.Log("Switch " + index + " reset as part of chain reset");
		m_switches[index - 1].ResetChain();
	}

	// Update is called once per frame
	void Update () {
		if (DEBUG_KEYS) {
			if (Input.GetKeyDown(KeyCode.Alpha0)) {
				gameObject.BroadcastMessage("CollectKeyCard", KeyCards.Red);
			} else if (Input.GetKeyDown(KeyCode.Alpha1)) {
				gameObject.BroadcastMessage("CollectKeyCard", KeyCards.Green);
			} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				gameObject.BroadcastMessage("CollectKeyCard", KeyCards.Blue);
			} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
				gameObject.BroadcastMessage("CollectKeyCard", KeyCards.Magenta);
			} else if (Input.GetKeyDown(KeyCode.Alpha4)) {
				gameObject.BroadcastMessage("CollectKeyCard", KeyCards.Yellow);
			} else if (Input.GetKeyDown(KeyCode.Alpha5)) {
				gameObject.BroadcastMessage("CollectKeyCard", KeyCards.Cyan);
			} else if (Input.GetKeyDown(KeyCode.Q)) {
				gameObject.BroadcastMessage("ToggleSwitch", 1);
			} else if (Input.GetKeyDown(KeyCode.W)) {
				gameObject.BroadcastMessage("ToggleSwitch", 2);
			} else if (Input.GetKeyDown(KeyCode.E)) {
				gameObject.BroadcastMessage("ToggleSwitch", 3);
			} else if (Input.GetKeyDown(KeyCode.R)) {
				gameObject.BroadcastMessage("ToggleSwitch", 4);
			} else if (Input.GetKeyDown(KeyCode.T)) {
				gameObject.BroadcastMessage("ToggleSwitch", 5);
			} else if (Input.GetKeyDown(KeyCode.Y)) {
				gameObject.BroadcastMessage("ToggleSwitch", 6);
			} else if (Input.GetKeyDown(KeyCode.U)) {
				gameObject.BroadcastMessage("ToggleSwitch", 7);
			} else if (Input.GetKeyDown(KeyCode.I)) {
				gameObject.BroadcastMessage("ToggleSwitch", 8);
			} else if (Input.GetKeyDown(KeyCode.O)) {
				gameObject.BroadcastMessage("ToggleSwitch", 9);
			} else if (Input.GetKeyDown(KeyCode.P)) {
				gameObject.BroadcastMessage("ToggleSwitch", 10);
			}
		}
	}
}