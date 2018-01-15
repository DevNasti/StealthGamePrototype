using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

	public State currentState;
	public Transform eyes; //empty gameObject in front of the guard, which is the point where are the "eye"
	public State remainInState; // fake state, there will no be transition

	private bool aiActive = true;

	[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public List<Vector3> waypointList;
	[HideInInspector] public int nextWaipoint;
	[HideInInspector] public Transform player;

	void Awake (){ 
		navMeshAgent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
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

	public void TransitionToState(State nextState){
		if (nextState != remainInState) {
			currentState = nextState;
		}
	}

	public bool CheckIfCountdownElapsed(){	//used for timing actions, in this time for making the player shoot only after a certain amount of time
	}
}
