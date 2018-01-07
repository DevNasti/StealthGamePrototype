using UnityEngine;

public class Player : MonoBehaviour {

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

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		inputDirection = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

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
}
