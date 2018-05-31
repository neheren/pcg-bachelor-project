using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemLogic : MonoBehaviour {
	public Transform a, b, c;
	public int ay, by, cy;
	objectState state;

	void Start () {
		state = GetComponent<objectState> ();
		a = transform.GetChild (0);
		b = transform.GetChild (1);
		c = transform.GetChild (2);
	
		while (isSameAngle ()) {
			scrambleRotation ();
		}
	}

	void scrambleRotation () {
		a.GetComponent<SmoothRotator> ().scramble ();
		b.GetComponent<SmoothRotator> ().scramble ();
		c.GetComponent<SmoothRotator> ().scramble ();
	}

	bool runOnce = false;

	void Update () {
		if (!runOnce && isSameAngle ()) {
			runOnce = true;
			state.triggered = true;
			state.parent.triggered = true;
		}
	}

	bool isSameAngle () {
		ay = Mathf.Abs (Mathf.RoundToInt (a.rotation.y * 2));
		by = Mathf.Abs (Mathf.RoundToInt (b.rotation.y * 2));
		cy = Mathf.Abs (Mathf.RoundToInt (c.rotation.y * 2));
		return  ay == by && by == cy;
	}
}
	       

		
        


    
        