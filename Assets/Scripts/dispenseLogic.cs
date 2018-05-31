using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dispenseLogic : MonoBehaviour {

	GameObject cube;
	objectState oState;
	bool runOnce = false;

	// Use this for initialization
	void Start () {
		oState = GetComponent<objectState> ();
		cube = transform.GetChild (2).gameObject;
		cube.GetComponent<objectState> ().parent = oState.parent; // transfering parent 
		cube.SetActive (false);
        
	}

	// Update is called once per frame
	void Update () {
		if (!runOnce && oState.triggered) {
			cube.SetActive (true);
			GetComponent<AudioSource> ().Play ();
			runOnce = true;
		}
	}

}
