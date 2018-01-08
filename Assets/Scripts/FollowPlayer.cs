using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform target;



	void LateUpdate(){
		transform.position = Vector3.Lerp ( transform.position, new Vector3(target.position.x, transform.position.y, target.position.z-4), 1f);
	}
}
