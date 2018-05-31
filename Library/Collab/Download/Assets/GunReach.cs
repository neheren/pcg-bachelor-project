using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReach : MonoBehaviour {

	public GameObject GunObject;
	public bool GunIsReady = false;
	public GameObject player;
	public Camera playerCam;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		playerCam = player.GetComponentInChildren<Camera> ();
		GunObject = playerCam.transform.GetChild (1).gameObject;
		GunObject.SetActive (false);
		GunIsReady = true;
	}


	Vector2 widthThresold = new Vector2 (0, Screen.width);
	Vector2 heightThresold = new Vector2 (0, Screen.height);
	public bool gunUp;
	public bool insideArea;
	public bool lookingAt;
	public bool dotPositive;

	void Update () {
		insideArea = Vector3.Distance (transform.position, player.transform.position) < 5;
		Vector2 screenPosition = playerCam.WorldToScreenPoint (transform.position + new Vector3 (0f, 1.5f, 0f));
		dotPositive = Vector3.Dot (transform.position - player.transform.position, player.transform.forward) > 0;
		lookingAt = !(screenPosition.x < widthThresold.x || screenPosition.x > widthThresold.y || screenPosition.y < heightThresold.x || screenPosition.y > heightThresold.y);
		if (dotPositive && lookingAt && insideArea) {
			if (!gunUp) {
				gunUp = true;
				OnBecameVisible ();
			}
		} else {
			if (gunUp) {
				gunUp = false;
				OnBecameInvisible ();
				
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
		player.transform.gameObject.GetComponentInChildren<animationController> (true).allowShooting = true;

	}

	Coroutine gunDown;

	void OnBecameInvisible () {
		gunDown = StartCoroutine (player.transform.gameObject.GetComponentInChildren<animationController> (true).animateAndDisable ());
		player.transform.gameObject.GetComponentInChildren<animationController> (true).allowShooting = false;
	}

	void OnDestroy () {
		try {
			player.transform.gameObject.GetComponentInChildren<animationController> (true).allowShooting = false;
		} catch {
		}
	}

}

