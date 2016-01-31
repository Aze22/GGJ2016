using UnityEngine;
using System.Collections;

public class WaypointScript : MonoBehaviour {

	public float m_waitHere = 1f;
	private float m_waited = 0f;

	public Color unselectedColor;
	public Color selectedColor;

	[HideInInspector]
	public GameObject[] checkpoints;

	// Use this for initialization
	void Start () {
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void OnDrawGizmos()
	{
		OnDrawGizmos(false);
	}
	public void OnDrawGizmosSelected()
	{
		OnDrawGizmos(true);
	}
	public void OnDrawGizmos(bool selected)
	{
		unselectedColor = new Color(1f, 0.8f, 0.2f, 0.9f);
		unselectedColor = new Color(1f, 0.8f, 0.2f, 0.4f);

		checkpoints = new GameObject[transform.parent.childCount];

		for (int i = 0; i < transform.parent.childCount; i++)
		{
			checkpoints[i] = transform.parent.GetChild(i).gameObject;
		}

		if (checkpoints == null)
			return;

		Gizmos.color = selected ? new Color(selectedColor.r, selectedColor.g, selectedColor.b, selectedColor.a)
			: new Color(unselectedColor.r, unselectedColor.g, unselectedColor.b, unselectedColor.a);

		for (int i = 0; i < checkpoints.Length; ++i)
		{
			if (checkpoints[i] != null)
			{
				Vector3 cubeSize = new Vector3(0.3f, 0.3f, 0.3f);

				Gizmos.DrawCube(checkpoints[i].transform.position, cubeSize);
				Gizmos.DrawLine(checkpoints[i].transform.position, checkpoints[(i + 1) % checkpoints.Length].transform.position);
			}
		}

	}
}
