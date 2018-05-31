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
	}

	void Update () {


		if ((Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.E)) && Physics.Raycast (this.transform.position, this.transform.forward, out whatIHit, shootLenght)) {
			if (cubeHit.distance < distanceToSee) {
				if (whatIHit.collider.gameObject.tag == "totem_cube") {
					StartCoroutine (whatIHit.transform.gameObject.GetComponent<SmoothRotator> ().Rotate (Vector3.up, 90, 1.0f));
					Debug.DrawRay (this.transform.position, this.transform.forward * distanceToSee, Color.magenta, 10f);
				}
				if (whatIHit.transform.tag == "button") {
					whatIHit.transform.gameObject.GetComponent<buttonBehavouir> ().trigger ();
					Debug.DrawRay (this.transform.position, this.transform.forward * distanceToSee, Color.black, 10f);
				}
			}

			if (transform.GetChild (1).GetComponentInChildren<animationController> ().shot ()) {
				StartCoroutine (moveBack (transform.forward));
			}
			print (whatIHit.transform.name);

			if (whatIHit.transform.tag == "CompCube") {
				print ("hit comp cube");
				if (whatIHit.transform.parent.gameObject.GetComponent<GunReach> ().GunIsReady == true) {
					print ("hi2");
					whatIHit.transform.gameObject.GetComponent<CompCubeHit> ().IsHit ();
					Debug.DrawRay (this.transform.position, this.transform.forward * distanceToSee, Color.red, 10f);
				}
			}
		}




	}

	IEnumerator moveBack (Vector3 dir) {
		transform.parent.transform.position -= dir / 20f;
		yield return new WaitForEndOfFrame ();
		transform.parent.transform.position -= dir / 30f;
		yield return new WaitForEndOfFrame ();
	}

}
