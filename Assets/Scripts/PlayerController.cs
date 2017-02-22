using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject HeadFirstPerson, HeadThirdPerson;

	public float moveSpeed = 1f;
	public float sprintingModifier = 2f;

	//public Animator anim;

	private CursorLockMode cursorMode;
	private bool isSprinting = false;
	private ViewMode currentViewMode;

	private enum ViewMode {FirstPerson, ThirdPerson};


	// Use this for initialization
	void Start () {
		cursorMode = CursorLockMode.Locked;
		currentViewMode = ViewMode.ThirdPerson;
	}
	
	// Update is called once per frame
	void Update () {

		// get inputs
		var move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		move *= moveSpeed;
		isSprinting = Input.GetButton("Run");
		if(isSprinting) { move *= sprintingModifier; }
		var rotationBody = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);
		var rotationHead = new Vector3(Input.GetAxis("Mouse Y"), 0f, 0f);

		
		//ApplyTransformations(move, rotationBody, rotationHead);	
	}

	private void ApplyTransformations(Vector3 movement, Vector3 rotationBody, Vector3 rotationHead) {
		

		switch(currentViewMode) {
			case ViewMode.FirstPerson:
			{
				this.gameObject.transform.Rotate(rotationBody);
				HeadFirstPerson.transform.Rotate(rotationHead);
				// clamp rotation
				HeadFirstPerson.transform.rotation = ClampRotationAroundXAxis(HeadFirstPerson.transform.rotation, -90f, 90f);
				// force camera to look forward relative to body
				var rot = HeadFirstPerson.transform.localEulerAngles;
				rot.y = 0f;
				rot.z = 0f;
				HeadFirstPerson.transform.localEulerAngles = rot;
				break;
			}
			case ViewMode.ThirdPerson:
			{
				this.gameObject.transform.Rotate(rotationBody);
				HeadThirdPerson.transform.Rotate(rotationHead);

				// clamp rotation
				HeadThirdPerson.transform.rotation = ClampRotationAroundXAxis(HeadThirdPerson.transform.rotation, -10f, 40f);
				var rot = HeadThirdPerson.transform.localEulerAngles;
				rot.y = 0f;
				rot.z = 0f;
				HeadThirdPerson.transform.localEulerAngles = rot;
				break;
			}
			default:
			break;
		}
		var camForward = Camera.main.transform.forward;
		
		//anim.SetBool("isWalking", movement.magnitude != 0f);
		//anim.SetFloat("Blend", movement.normalized.magnitude);
		
		this.gameObject.transform.Translate(movement);
	}

	private Quaternion ClampRotationAroundXAxis(Quaternion q, float min, float max)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

        angleX = Mathf.Clamp (angleX, min, max);

        q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
