using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getColorFromParent : MonoBehaviour {
	public objectState parentO;
	// Use this for initialization
	void Start () {
		
	}

	void OnEnable () {
		GetComponent<objectState> ().changeColor (parentO.col);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
