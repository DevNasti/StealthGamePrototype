using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

	public State currentState;
	public Transform eyes; //empty gameObject in front of the guard, which is the point where are the "eye"


	private bool aiActive = true;

	[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public List<Vector3> waypointList;
	[HideInInspector] public int nextWaipoint;

	void Awake (){ 
		navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
		if (!aiActive)
			return;
		currentState.UpdateState (this);
	}

	void OnDrawGizmos(){
		if (currentState != null) {
			Gizmos.color = currentState.sceneGizmoColor;
			Gizmos.DrawWireSphere (eyes.position, .5f);
		}
	}
}
