using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {

    public Transform pathHolder;
	public LayerMask viewMask;
    float speed = 5f;
    float waitTime = .3f;
    float rotationSpeed = 90f; //90deg per second

	public Light spotLight;
	public float viewDistance = 8f;
	float viewAngle;
	Color originalSpotLightColor;

	Transform player;

    void Start()
    {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		viewAngle = spotLight.spotAngle;
		originalSpotLightColor = spotLight.color;

		var wayPoints = new Vector3[pathHolder.childCount];

		for (int i = 0; i < wayPoints.Length; i++){
            wayPoints[i] = pathHolder.GetChild(i).position;
            wayPoints[i] = new Vector3(wayPoints[i].x, transform.position.y, wayPoints[i].z);
        	}
        StartCoroutine(FollowPath(wayPoints));
    }

	void Update(){
		if (CanSeePlayer ()) {
			spotLight.color = Color.red;
		}
		else {
			spotLight.color = originalSpotLightColor;
		}
	}

    IEnumerator FollowPath(Vector3[] wayPoints)
    {
        transform.position = wayPoints[0];
        int targetWayPointIndex = 1;

        var targetWayPoint = wayPoints[targetWayPointIndex];
        transform.LookAt(targetWayPoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            if (transform.position == targetWayPoint)
            {
                targetWayPointIndex = (targetWayPointIndex + 1) % wayPoints.Length;
                targetWayPoint = wayPoints[targetWayPointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnTowards(targetWayPoint));
            }
            yield return null;
        }
    }

    private IEnumerator TurnTowards(Vector3 lookTarget)
    {
        var dirLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirLookTarget.z, dirLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f){
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, rotationSpeed *Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

	private bool CanSeePlayer(){
		if(Vector3.Distance(transform.position, player.position) < viewDistance){
			Vector3 dirToPlayer = (player.position - transform.position).normalized;
			float angleBetweenGuardAndPlayer = Vector3.Angle (transform.forward, dirToPlayer);

			if (angleBetweenGuardAndPlayer < viewAngle / 2f) {
				if (!Physics.Linecast (transform.position, player.position, viewMask))
					return true;
				
			}
		}
		return false;
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

		Gizmos.color = Color.red;
		Gizmos.DrawRay (transform.position, transform.forward* viewDistance); 
    }
   
}
