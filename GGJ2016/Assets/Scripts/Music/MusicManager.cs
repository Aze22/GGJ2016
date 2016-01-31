using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour {
	static MusicManager instance = null;

	public AudioClip[] levelMusicChangeArray;

	private AudioSource music;

	void Awake () {
		Debug.Log ("Don't destroy on load: " + name);
	}

	void Start () {
		Debug.Log("Started MusicManager");

		// Check for an existing MusicManager
		if ((instance) && (instance != this)) {
			Destroy(gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			OnLevelWasLoaded(SceneManager.GetActiveScene().buildIndex);
		}
	}

	private int GetIntPrefix(string input) {
		int output = 0;

		for (int index = 0; index < input.Length; index++) {
			char letter = input[index];

			if ((letter >= '0') && (letter <= '9')) {
				output *= 10;
				output += letter - '0';
			}
			else {
				break;
			}
		}

		return output;
	}

	public void ChangeVolume(float volume) {
		if ((volume < 0f) || (volume > 1f)) {
			Debug.LogError("Invalid volume: " + volume);
		} else {
			music.volume = volume;
		}
	}

	void OnLevelWasLoaded (int level) {
		int levelPrefix = GetIntPrefix(SceneManager.GetActiveScene().name);

		if (levelPrefix >= levelMusicChangeArray.Length) {
			Debug.Log("MusicManager: no music for level " + levelPrefix);
			return;
		}

		AudioClip levelMusic = levelMusicChangeArray[levelPrefix];
		Debug.Log("MusicManager: level " + level + " (" + levelPrefix + "); playing " + levelMusic);

		if ((levelMusic) && (levelMusic != music.clip)) {
			music.clip = levelMusic;
			music.Stop ();
			music.loop = true;

			if (level == 0)
			{
				music.loop = false;
			}

			music.Play ();
		}
	}
}
