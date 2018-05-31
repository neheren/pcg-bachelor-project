using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

public class DestroyCube : MonoBehaviour {

	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController Controller;
	public MoveObject MoveObject;

	private void Start () {
		Controller = GameObject.Find ("FPSController").GetComponent<FirstPersonController> ();
		Controller.tag = "Player";
		//Cube = GameObject.Find("CUBE");
	}

	void OnTriggerEnter (Collider other) {

		if (other.transform.gameObject.tag == "Player") {
			print ("destroyCube");
			Destroy (other.gameObject.transform.GetChild (0).GetChild (0).GetChild (0).gameObject);
		}
	}

}
