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


		if ((Input.GetMouseButtonUp (0) || Input.GetKeyDown (KeyCode.E)) && Physics.Raycast (this.transform.position, this.transform.forward, out whatIHit, distanceToSee)) {
			Debug.DrawRay (this.transform.position, this.transform.forward * distanceToSee, Color.magenta, 10f);
			//			Debug.Log ("I touched " + whatIHit.transform.gameObject.tag);

			if (whatIHit.collider.gameObject.tag == "totem_cube") {
				StartCoroutine (whatIHit.transform.gameObject.GetComponent<SmoothRotator> ().Rotate (Vector3.up, 90, 1.0f));
			}
			if (whatIHit.transform.tag == "button") {
				whatIHit.transform.gameObject.GetComponent<buttonBehavouir> ().trigger ();

			}

		}

		if ((Input.GetMouseButtonDown (0) && Physics.Raycast (this.transform.position, this.transform.forward, out cubeHit, shootLenght))) {
			transform.GetChild (1).GetComponentInChildren<animationController> ().shot ();
			if (cubeHit.transform.tag == "CompObject") {
				cubeHit.transform.gameObject.GetComponent<CompCubeHit> ().IsHit ();
				
				if (cubeHit.transform.tag == "CompCube" && GunReach.GunIsReady == true) {
					Debug.DrawRay (this.transform.position, this.transform.forward * distanceToSee, Color.red, 10f);
				}
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
}
