using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRaycasting : MonoBehaviour {

	public float distanceToSee;
 
	float shootLenght = 200;
	public bool finalCubeHit = false;
	public GunReach GunReach;
	RaycastHit whatIHit;
	RaycastHit checkForDoor;
	RaycastHit cubeHit;

   

	void Start () {
		GunReach = GameObject.FindWithTag ("CompObject").GetComponent<GunReach> ();
	}

	void Update () {


		if ((Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.E)) && Physics.Raycast (this.transform.position, this.transform.forward, out whatIHit, shootLenght)) {
			if (cubeHit.distance < distanceToSee) {
				Debug.DrawRay (this.transform.position, this.transform.forward * distanceToSee, Color.magenta, 10f);
				if (whatIHit.collider.gameObject.tag == "totem_cube") {
					StartCoroutine (whatIHit.transform.gameObject.GetComponent<SmoothRotator> ().Rotate (Vector3.up, 90, 1.0f));
				}
				if (whatIHit.transform.tag == "button") {
					whatIHit.transform.gameObject.GetComponent<buttonBehavouir> ().trigger ();
				}
			}

			if (transform.GetChild (1).GetComponentInChildren<animationController> ().shot ()) {
				StartCoroutine (moveBack (transform.forward));
			}
			print ("hit1");
			print (whatIHit.transform.parent);
			if (whatIHit.transform.parent.gameObject.GetComponent<GunReach> ().GunIsReady == true) {
				print ("gun ready");
			}
			if (whatIHit.transform.tag == "CompCube" && whatIHit.transform.parent.gameObject.GetComponent<GunReach> ().GunIsReady == true) {
				print ("hi2");
				whatIHit.transform.gameObject.GetComponent<CompCubeHit> ().IsHit ();
				Debug.DrawRay (this.transform.position, this.transform.forward * distanceToSee, Color.red, 10f);
			}

			if (whatIHit.transform.tag == "CompObject") {

			}
		}


		//	void FixedUpdate () {
		//		if (Physics.Raycast (this.transform.position, this.transform.forward, out checkForDoor, 2)) {
		//			if (checkForDoor.transform.tag == "door") {
		//				print ("raycast door");
		//				checkForDoor.transform.gameObject.GetComponent<TriggerRoomGeneration> ().generateRoomHere ();
		//			}
		//		}
		//	}

	}

	IEnumerator moveBack (Vector3 dir) {
		transform.parent.transform.position -= dir / 8f;
		yield return new WaitForEndOfFrame ();
		transform.parent.transform.position -= dir / 12f;
		yield return new WaitForEndOfFrame ();
		transform.parent.transform.position -= dir / 16f;
		yield return new WaitForEndOfFrame ();
	}

}
