using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision {

	public override bool Decide (StateController controller){
		bool chaseTargetIsActive = controller.player.gameObject.activeSelf; //i check if the player is still alive
		return chaseTargetIsActive;
	}
}
