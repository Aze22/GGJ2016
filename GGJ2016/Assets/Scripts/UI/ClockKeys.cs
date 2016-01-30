using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClockKeys : MonoBehaviour {
	public Image[] keyImages;

	// Make sure none of the keys are collected at the start
	void Start() {
		for (int index = 0; index < keyImages.Length; index++) {
			keyImages[index].enabled = false;
		}
	}

	// Collect a key
	public void CollectKey(GameStateManager.KeyCards index) {
		keyImages[((int)index) - 1].enabled = true;
	}
}