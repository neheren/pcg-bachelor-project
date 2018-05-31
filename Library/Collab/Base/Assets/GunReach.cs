using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReach : MonoBehaviour {

	public GameObject GunObject;
	public bool GunIsReady = false;
	public GameObject player;
	Camera playerCam;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		playerCam = player.GetComponentInChildren<Camera> ();
		GunObject = GameObject.Find ("GunPos");
		GunObject.SetActive (false);
	}

	private void OnTriggerEnter (Collider other) {
		if (other.transform.gameObject.tag == "Player") {
			GunIsReady = true;
		}
	}

	private void OnTriggerExit (Collider other) {
		if (other.transform.gameObject.tag == "Player") {
			GunIsReady = false;
			if (gunUp)
				StartCoroutine (other.transform.gameObject.GetComponentInChildren<animationController> ().animateAndDisable ());
		}
	}

	Vector2 widthThresold = new Vector2 (0, Screen.width);
	Vector2 heightThresold = new Vector2 (0, Screen.height);
	public bool gunUp;

	void Update () {
		if (GunIsReady) {
			Vector2 screenPosition = playerCam.WorldToScreenPoint (transform.position);
			bool dotPositive = Vector3.Dot (transform.position - player.transform.position, player.transform.forward) > 0;

			if (dotPositive && screenPosition.x < widthThresold.x || screenPosition.x > widthThresold.y || screenPosition.y < heightThresold.x || screenPosition.y > heightThresold.y) {
			
				if (gunUp) {
					gunUp = false;
					OnBecameInvisible ();
				}
		
			} else if (dotPositive) {
				if (!gunUp) {
					gunUp = true;
					OnBecameVisible ();
				}
			}
		}
	}

	void OnBecameVisible () {
		try {
			StopCoroutine (gunDown);
		} catch {

		}
		player.transform.GetChild (0).GetChild (1).gameObject.SetActive (false);
		player.transform.GetChild (0).GetChild (1).gameObject.SetActive (true);
		player.transform.gameObject.GetComponentInChildren<animationController> ().allowShooting = true;

	}

	Coroutine gunDown;

	void OnBecameInvisible () {
		gunDown = StartCoroutine (player.transform.gameObject.GetComponentInChildren<animationController> ().animateAndDisable ());
		player.transform.gameObject.GetComponentInChildren<animationController> ().allowShooting = false;
	}



}

