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
			instance.onSceneLoadedProxy(SceneManager.GetActiveScene().buildIndex);
			Destroy(gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			onSceneLoadedProxy(SceneManager.GetActiveScene().buildIndex);
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

	void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
         
	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void onSceneLoadedProxy(int level) {
		int levelPrefix = GetIntPrefix(SceneManager.GetActiveScene().name);
		OnSceneLoadedCommon(level, levelPrefix);
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		int levelPrefix = GetIntPrefix(scene.name);
		int level = scene.buildIndex;
		OnSceneLoadedCommon(level, levelPrefix);
	}

	void OnSceneLoadedCommon(int level, int levelPrefix) {
		if (levelPrefix >= levelMusicChangeArray.Length) {
			Debug.Log("MusicManager: no music for level " + levelPrefix);
			return;
		}

		AudioClip levelMusic = levelMusicChangeArray[SceneManager.GetActiveScene().buildIndex];
		Debug.Log("MusicManager: level " + level + " (" + levelPrefix + "); playing " + levelMusic);

		if ((levelMusic) && (music) && (levelMusic != music.clip)) {
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
