using UnityEngine;
using System.Collections;
using System;

public class EnemyScript : MonoBehaviour {

	public float m_normalSpeed;
	public float m_runningSpeed;
	public WaypointScript[] m_waypoints;
	private WaypointScript m_currentWaypoint;
	private int m_currentWaypointIndex = 0;
	private float m_currentDistanceFromWP = 0f;
	private Vector3 m_finalMovement;
	private Vector3 m_directionVector;

	private Transform m_meshT;
	private float m_waited = 0f;
	private enum State
	{
		Idle,
		Walking,
		Following
	}

	private State m_currentState;

	

	// Use this for initialization
	void Start ()
	{
		m_waited = 0f;

		if (m_waypoints.Length <= 0)
			Debug.LogError("NO WAYPOINTS SET FOR ENEMY " + transform.name + "!");

		for(int i = 0; i < m_waypoints.Length; i++)
		{
			if(m_waypoints[i] == null)
				Debug.LogError("WAYPOINT " + i + " IS MISSING OR DELETED FOR ENEMY " + transform.name + "!");
		}
		m_currentWaypoint = m_waypoints[0];
		m_currentState = State.Idle;
		m_meshT = transform.FindChild("Mesh");
		m_directionVector = m_meshT.forward;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_currentState == State.Idle)
			IdleUpdate();
		else if (m_currentState == State.Walking)
			Walking();
		else if (m_currentState == State.Following)
			FollowingUpdate();

		AdaptMeshRotation();
	}


	private void AdaptMeshRotation()
	{
		//m_meshT.LookAt(transform.position + m_directionVector);


		if (m_directionVector != Vector3.zero)
		{
			Vector3 targetDir = (transform.position + m_directionVector) - m_meshT.position;
			Vector3 newDir = Vector3.RotateTowards(m_meshT.forward, targetDir, Time.deltaTime * 20, 0f);
			m_meshT.transform.rotation = Quaternion.LookRotation(newDir);
			m_meshT.eulerAngles = new Vector3(0, m_meshT.eulerAngles.y, m_meshT.eulerAngles.z);
		}
	}
	

	private void FollowingUpdate()
	{
		throw new NotImplementedException();
	}

	private void Walking()
	{
		if (m_currentWaypoint != null)
		{
			m_currentDistanceFromWP = Vector3.Distance(transform.position, new Vector3(m_currentWaypoint.transform.position.x, transform.position.y, m_currentWaypoint.transform.position.z));

			if(m_currentDistanceFromWP > 0.1f)
			{
				m_finalMovement = Vector3.zero;
				m_finalMovement = ((m_currentWaypoint.transform.position - transform.position).normalized) * Time.deltaTime * m_normalSpeed;
				GetComponent<CharacterController>().Move(m_finalMovement);

				if (m_finalMovement != Vector3.zero)
					m_directionVector = m_finalMovement;
			}
			else
			{
				Debug.Log("Reach");
				m_waited = 0;
				m_currentState = State.Idle;
			}
		}
	}

	private void IdleUpdate()
	{
		if(m_currentWaypoint != null)
		{
			if(m_waited < m_currentWaypoint.m_waitHere)
			{
				m_waited += Time.deltaTime;
			}
			else
			{
				m_currentWaypointIndex++;
				if (m_currentWaypointIndex >= m_waypoints.Length)
					m_currentWaypointIndex = 0;

				m_currentWaypoint = m_waypoints[m_currentWaypointIndex];
				m_currentState = State.Walking;
			}
		}
	}

}
