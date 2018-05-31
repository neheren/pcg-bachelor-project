using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonBehavouir : MonoBehaviour {
	objectState state;
	// Use this for initialization
	void Start () {
		state = GetComponent<objectState> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void trigger () {
		state.changeColor (Color.cyan);
		objectState oState = GetComponent<objectState> ();
		oState.parent.triggered = true;
		oState.triggered = true;

	}
}
