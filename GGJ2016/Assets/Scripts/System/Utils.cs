using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Utils : MonoBehaviour
{

	#region Singleton

	private static Utils _instance;

	public static Utils Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Utils)FindObjectOfType(typeof(Utils));

				if (FindObjectsOfType(typeof(Utils)).Length > 1)
				{
					Debug.LogError("[Singleton] Something went really wrong " +
								   " - there should never be more than 1 singleton!" +
								   " Reopenning the scene might fix it.");
					return _instance;
				}

				if (_instance == null)
				{
					GameObject singleton = Instantiate(Resources.Load("Utils")) as GameObject;
					_instance = singleton.GetComponent<Utils>();
					singleton.name = "(singleton) " + typeof(Utils).ToString();

					DontDestroyOnLoad(singleton);
					_instance.Init();

					Debug.Log("[Singleton] An instance of " + typeof(Utils) +
							  " is needed in the scene, so '" + singleton +
							  "' was created with DontDestroyOnLoad.");
				}
				else
				{
					Debug.Log("[Singleton] Using instance already created: " +
							  _instance.gameObject.name);
				}
			}

			return _instance;
		}
	}

	public void Init()
	{
		//Awake();
	}

	#endregion


	public static Vector3 zeroUI = new Vector3(0.1f, 0.1f, 1f);
	public static Vector3 zeroUI_X = new Vector3(0.1f, 1f, 1f);
	public static Vector3 zeroUI_Y = new Vector3(1f, 0.1f, 1f);
	public static List<float> _deactivateIn = new List<float>();
	public static List<GameObject> objToDeactivateIn = new List<GameObject>();
	public static List<bool> destroyAfter = new List<bool>();
	public static List<bool> stateForObjIn = new List<bool>();
	public static List<bool> ignoreTimeScale = new List<bool>();


	/// <summary>
	/// Determine the signed angle between two vectors, with normal 'n'
	/// as the rotation axis.
	/// </summary>
	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(
			Vector3.Dot(n, Vector3.Cross(v1, v2)),
			Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
	}

	void Update()
	{
		List<bool> keepAtIndex;
		keepAtIndex = new List<bool>();

		int i = 0;
		foreach(GameObject obj in objToDeactivateIn)
		{
			if(_deactivateIn[i] > 0)
			{
				_deactivateIn[i] -= (ignoreTimeScale[i] ? Time.unscaledDeltaTime : Time.deltaTime);
				keepAtIndex.Add(true);
			}
			else 
			{
				_deactivateIn[i] = 0;

				if (objToDeactivateIn[i]!= null)
					objToDeactivateIn[i].SetActive(stateForObjIn[i]);

				if(destroyAfter[i])
					Destroy(obj);

				keepAtIndex.Add(false);
			}
			i++;
		}

		int u = 0;
		foreach(bool keepThisOne in keepAtIndex)
		{
			if(!keepThisOne)
			{
				objToDeactivateIn.RemoveAt(u);
				_deactivateIn.RemoveAt(u);
				stateForObjIn.RemoveAt(u);
				ignoreTimeScale.RemoveAt(u);
				destroyAfter.RemoveAt(u);
				u--;
			}
			u++;	
		}
	}

	public static bool inBuild()
	{
		return !Application.isEditor;
	}

	public IEnumerator setActiveIn(GameObject go, bool state, float delay)
	{
		yield return new WaitForSeconds(delay);
		go.SetActive(state);
	}

	public void MessengerSend(Messenger[] messengersToSend)
	{
		if(messengersToSend != null)
			StartCoroutine(SendNow(messengersToSend));
	
	}

    public void MessengerSend(MONO_Messenger[] messengersToSend)
    {
        if (messengersToSend != null)
            StartCoroutine(SendNow(messengersToSend));

    }

    public IEnumerator SendNow(Messenger[] messengers)
	{
		List<Messenger> messengersUnsorted = new List<Messenger>();
		List<Messenger> sortedMessengers = new List<Messenger>();

		foreach(Messenger currentMessenger in messengers)
		{
			messengersUnsorted.Add(currentMessenger);
		}

		sortedMessengers = messengersUnsorted.OrderBy(messenger => messenger.delay).ToList();

		float waited = 0;
		foreach(Messenger currentMessenger in sortedMessengers)
		{
			yield return new WaitForSeconds(currentMessenger.delay - waited);
			waited += currentMessenger.delay;
			currentMessenger.Send();
		}
	}

    public IEnumerator SendNow(MONO_Messenger[] messengers)
    {
        List<MONO_Messenger> messengersUnsorted = new List<MONO_Messenger>();
        List<MONO_Messenger> sortedMessengers = new List<MONO_Messenger>();

        foreach (MONO_Messenger currentMessenger in messengers)
        {
            messengersUnsorted.Add(currentMessenger);
        }

        sortedMessengers = messengersUnsorted.OrderBy(messenger => messenger.delay).ToList();

        float waited = 0;
        foreach (MONO_Messenger currentMessenger in sortedMessengers)
        {
            yield return new WaitForSeconds(currentMessenger.delay - waited);
            waited += currentMessenger.delay;
            currentMessenger.Send();
        }
    }

    public static object TryParseEnum(System.Type enumType, string value, out bool success)
	{
		success = Enum.IsDefined(enumType, value);
		if (success)
		{
			return Enum.Parse(enumType, value);
		}
		return null;
	}



	void Awake()
	{
		/*_deactivateIn = new List<float>();
		objToDeactivateIn = new List<GameObject>();
		destroyAfter = new List<bool>();
		stateForObjIn = new List<bool>();
		ignoreTimeScale = new List<bool>();*/
	}
}

public static class PlayerPrefsExtensions
{
	public static void SetBool(string key, bool valueToSet)
	{
		int valToSet = 0;
		if(valueToSet)
			valToSet = 1;
		
		PlayerPrefs.SetInt(key, valToSet);
	}
	
	public static void SetVector3(string key, Vector3 valueToSet)
	{
		PlayerPrefs.SetFloat(key + "_X", valueToSet.x);
		PlayerPrefs.SetFloat(key + "_Y", valueToSet.y);
		PlayerPrefs.SetFloat(key + "_Z", valueToSet.z);
	}
	
	public static Vector3 GetVector3(string key)
	{
		return new Vector3(PlayerPrefs.GetFloat(key + "_X"), PlayerPrefs.GetFloat(key + "_Y"), PlayerPrefs.GetFloat(key + "_Z"));
	}
	
	public static bool GetBool(string key)
	{
		int valToGet = PlayerPrefs.GetInt(key);
		if(valToGet == 1)
			return true;
		else
			return false;
	}
}

public static class GameObjectExtensions
{
	public static void SetActive(this GameObject go, bool state, float delay)
	{
		SetActive(go, state, delay, false, false);
	}

	public static void SetActive(this GameObject go, bool state, float delay, bool ignoreTimeScale, bool destroyAfter)
	{
		Utils.Instance.Init();
		Utils._deactivateIn.Add(delay);
		Utils.objToDeactivateIn.Add(go);
		Utils.stateForObjIn.Add(state);
		Utils.ignoreTimeScale.Add(ignoreTimeScale);
		Utils.destroyAfter.Add(destroyAfter);
	}
}

public static class TransformExtensions
{
	public static void Save(this Transform transform)
	{
		PlayerPrefsExtensions.SetVector3(transform.name + "_SavedPosition", transform.position);
		PlayerPrefsExtensions.SetVector3(transform.name + "_SavedRotation", transform.eulerAngles);
	}

	public static void Load(this Transform transform)
	{
		transform.position = PlayerPrefsExtensions.GetVector3(transform.name + "_SavedPosition");
		transform.eulerAngles = PlayerPrefsExtensions.GetVector3(transform.name + "_SavedRotation");
	}

	public static void SetXPosition(this Transform transform, float newX)
	{
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

	public static void SetYPosition(this Transform transform, float newY)
	{
		transform.position = new Vector3(transform.position.x, newY, transform.position.z);
	}

	public static void SetZPosition(this Transform transform, float newZ)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
	}

	public static void SetLocalXPosition(this Transform transform, float newX)
	{
		transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
	}
	
	public static void SetLocalYPosition(this Transform transform, float newY)
	{
		transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
	}
	
	public static void SetLocalZPosition(this Transform transform, float newZ)
	{
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newZ);
	}
}
