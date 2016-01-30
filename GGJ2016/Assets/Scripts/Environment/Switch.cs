﻿using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
	// Call this function to set the switch state
	// This function should update the switch asset to an appropriate state (e.g. change graphic)
	public void SetState(bool state) {
		Debug.Log("Setting switch state to " + state);
		if (state) {
			gameObject.GetComponent<SpriteRenderer>().color = Color.red;
		} else {
			gameObject.GetComponent<SpriteRenderer>().color = Color.green;
		}
	}
}