using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Image backgroundImage;
	private Image joystickImage;
	private Vector3 inputVector;

	void Start(){
		backgroundImage = GetComponent<Image> ();
		joystickImage = transform.GetChild(0).GetComponent<Image>();
	}

	public virtual void OnDrag(PointerEventData ped){
		Vector2 pos;

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (backgroundImage.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / backgroundImage.rectTransform.sizeDelta.x);
			pos.y = (pos.y / backgroundImage.rectTransform.sizeDelta.y);

			inputVector = new Vector3 (pos.x * 2 + 1, 0, pos.y * 2 - 1);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			//move joystickImage
			joystickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x * (backgroundImage.rectTransform.sizeDelta.x/3), inputVector.z *(backgroundImage.rectTransform.sizeDelta.x/3));


			Debug.Log (inputVector);
		}
	}

	public virtual void OnPointerDown(PointerEventData ped){
		OnDrag (ped);
	}


	public virtual void OnPointerUp(PointerEventData ped){
		inputVector = Vector3.zero;
		joystickImage.rectTransform.anchoredPosition = Vector3.zero;
	}

	public float Horizontal(){
		if (inputVector.x != 0)
			return inputVector.x;
		else
			return Input.GetAxisRaw ("Horizontal");
		
	}

	public float Vertical(){
		if (inputVector.z != 0)
			return inputVector.z;
		else
			return Input.GetAxisRaw ("Vertical");
	}


}