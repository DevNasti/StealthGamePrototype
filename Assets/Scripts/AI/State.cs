using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/State")]
public  class State : ScriptableObject {

	public AbstractAction[] actions;
	public Color sceneGizmoColor = Color.grey; //is grey so it's easy to understand is not set

	public void UpdateState(StateController controller){
		DoActions (controller);
	}

	private void DoActions(StateController controller){
		foreach (AbstractAction a in actions)
			a.Act (controller);
	}
}
