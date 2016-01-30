using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    public static GameStateManager Instance;
    public PlayerScript player;
    public StatusText statusManager;
    public ClockKeys keyManager;

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
		keyManager.CollectKey(index);
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

	// Used for debug purposes only
	void Update () {
		if (DEBUG_KEYS) {
			if (Input.GetKeyDown(KeyCode.R)) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}
}