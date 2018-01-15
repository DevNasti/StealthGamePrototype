using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : AbstractAction {

	public override void Act(StateController controller){
		Patrol (controller);
	}

	private void Patrol(StateController controller){
		controller.navMeshAgent.destination = controller.waypointList [controller.nextWaipoint];
		controller.navMeshAgent.isStopped = false;

		if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending) {
			controller.nextWaipoint = (controller.nextWaipoint + 1) % controller.waypointList.Count; 
			//the part over the "%", resets the nextWaypoint counter if it has exceded the lenght of the waypointList
		}
			
	}
}
