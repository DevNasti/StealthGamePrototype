using UnityEngine;

public class Player : MonoBehaviour {

	public VirtualJoystick js;

    public float moveSpeed = 5f;
	public float smoothTime = .1f;
	public float turnSpeed = 8f;

	Rigidbody rb;

	private Vector3 inputDirection;
	private float inputMagnitude, smoothInputMagnitute;
	private float targetAngle;
	private float smoothVelocity;
	private float angle;
	private Vector3 velocity = Vector3.zero;

	bool disabledPlayer; //the player is disabled when a game over occurs

	void Start(){
		rb = GetComponent<Rigidbody> ();

		Guard.OnGuardHasSpottedPlayer += Disable;
	}

	void Update () {
		inputDirection = Vector3.zero;

		if (!disabledPlayer) {
			if(js!=null)
				inputDirection = new Vector3 (js.Horizontal(), 0, js.Vertical()).normalized;
			else 
				inputDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;

		}

		inputMagnitude = inputDirection.magnitude;
		smoothInputMagnitute = Mathf.SmoothDamp(smoothInputMagnitute, inputMagnitude, ref smoothVelocity, smoothTime);

		targetAngle = Mathf.Atan2 (inputDirection.x, inputDirection.z) * Mathf.Rad2Deg; 

		angle = Mathf.LerpAngle (angle, targetAngle, turnSpeed * Time.deltaTime *inputMagnitude);

		transform.eulerAngles = Vector3.up * angle;
		transform.Translate (transform.forward * moveSpeed * Time.deltaTime * smoothInputMagnitute, Space.World);

		velocity = transform.forward * moveSpeed * smoothInputMagnitute;
	}



	void FixedUpdate(){
		rb.MoveRotation(Quaternion.Euler(Vector3.up*angle));
		rb.MovePosition(rb.position + velocity * Time.deltaTime);

		if (Input.GetButtonDown ("Jump"))
			rb.AddForce (Vector3.up * 2f, ForceMode.Impulse);
	}

	void Disable(){
		disabledPlayer = true;
	}

	void OnDestroy(){
		Guard.OnGuardHasSpottedPlayer -= Disable;
	}
}
