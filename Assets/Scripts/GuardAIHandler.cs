using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAIHandler : MonoBehaviour {

	public Transform pathHolder;

	private StateController state;

	void Start(){
		state = GetComponent<StateController> ();

		Vector3 aux;

		if(state.waypointList == null)	//if empty i initialize the waypoint list
			state.waypointList = new List<Vector3>();
		
		for (int i = 0; i < pathHolder.childCount; i++)	//i set all the wayPoints, that are stored as childs of "PathHolder"
			state.waypointList.Add(pathHolder.GetChild(i).position);


	}
	


	private void OnDrawGizmos()
	{
		var startPosition = pathHolder.GetChild(0).position;
		var previousPosition = startPosition;
		foreach (Transform waypoint in pathHolder){
			Gizmos.DrawSphere(waypoint.position, .2f);
			Gizmos.DrawLine(previousPosition, waypoint.position);
			previousPosition = waypoint.position;
		}
		Gizmos.DrawLine(previousPosition, startPosition);

		//Gizmos.color = Color.red;
		//Gizmos.DrawRay (transform.position, transform.forward* viewDistance);
	}
}
