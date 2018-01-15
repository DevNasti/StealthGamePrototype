using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/State")]
public  class State : ScriptableObject {

	public AbstractAction[] actions;
	public Color sceneGizmoColor = Color.grey; //is grey so it's easy to understand is not set
	public Transition[] transitions;

	public void UpdateState(StateController controller){
		DoActions (controller);
		CheckTransitions (controller);
	}

	private void DoActions(StateController controller){
		foreach (AbstractAction a in actions)
			a.Act (controller);
	}

	private void CheckTransitions(StateController controller){	//each frame i evaluate all the decisions
		bool decisionSucceded;
		foreach (Transition t in transitions) {
			decisionSucceded = t.decision.Decide (controller);

			if (decisionSucceded)
				controller.TransitionToState (t.trueState);
			else
				controller.TransitionToState (t.falseState);
		}
			
	}
}
