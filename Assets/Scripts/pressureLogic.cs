using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressureLogic : MonoBehaviour {
	public bool playerTriggers;
	public bool objectTriggers;
	public bool toggle;


	objectState oState;

	private void Start () {
		oState = GetComponent<objectState> ();
		try {
			oState.changeColor (oState.child.changeColor ());
			
		} catch {

			print ("couldn't set color for " + name);
		}
	}

	public void trigger () { // called by player on hit
		if (playerTriggers) { 
			oState.parent.triggered = true;
			oState.triggered = true;
		}
	}

	void OnCollisionEnter (Collision col) {
		//print (col.transform.tag);
		if (objectTriggers && col.transform.tag == "cube" && col.gameObject.GetComponent<objectState> ().parent == oState) {
			oState.parent.triggered = true;
			oState.triggered = true;
		}
	}

	void OnCollisionExit (Collision col) {
		if (objectTriggers && col.transform.tag == "cube") {
			objectState oState = GetComponent<objectState> ();
			oState.parent.triggered = false;
			oState.triggered = false;
    
		}
	}
}
