using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine.EventSystems;
//[RequireComponent(typeof(Collider))]
public class OnLookFireAnimParameters : MonoBehaviour
{


	[SerializeField]private string nameOfBool;
	private int boolHash;

	[SerializeField]private float minAngleFromDown;
	[SerializeField]private float maxAngleFromDown;

	private Collider thisCollider;
	private Transform camTransform;

	[Header("Events")]
	[SerializeField]private UnityEvent onSee;
	[SerializeField]private UnityEvent onUnSee;
//	[SerializeField]private UnityEvent onStart;

	[Header("References")]
	[SerializeField]private BoolVariable isStressMenuActive;
	[SerializeField]private Animator animatorToTrigger;

	void Start () {
		
		//onStart.Invoke();
		boolHash = Animator.StringToHash (nameOfBool);
		//thisCollider = GetComponent<Collider> ();
		camTransform = Camera.main.transform;
		//animatorToTrigger.SetBool (nameOfBool, true);
		//animatorToTrigger.SetBool (nameOfBool, false);
	}

//	public void OnPointerEnter(PointerEventData eventData){
//		
//		animatorToTrigger.SetBool (nameOfBool, true);
//		onSee.Invoke ();
//	}
//	public void OnPointerExit(PointerEventData eventData){
//		animatorToTrigger.SetBool (nameOfBool, false);
//		onUnSee.Invoke ();
//	}
	void Update () {

		if (isStressMenuActive.isOn){
			animatorToTrigger.SetBool (boolHash, true);
			return;
		}
			

		if (maxAngleFromDown > CameraAngleFromDown () && CameraAngleFromDown () > minAngleFromDown) {
			onSee.Invoke ();
			animatorToTrigger.SetBool (boolHash, true);

		} else {
			
			animatorToTrigger.SetBool (boolHash, false);
			onUnSee.Invoke ();
		}
//		RaycastHit hit;
//		Ray ray = new Ray (camTransform.position, camTransform.rotation * Vector3.forward);
//
//		if (thisCollider.Raycast (ray, out hit, 50f)) {
//			animatorToTrigger.SetBool (nameOfBool, true);
//			onSee.Invoke ();
//		} else {
//			
//			animatorToTrigger.SetBool (nameOfBool, false);
//			onUnSee.Invoke ();
//		}
//		

}
	private float CameraAngleFromDown(){
		return Vector3.Angle (Vector3.down, camTransform.rotation * Vector3.forward);}
}