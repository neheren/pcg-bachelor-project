using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class objectState : MonoBehaviour {

	public static Action<bool> onTriggeredCalled;

	private bool Triggered = false;

	public bool triggered {
		get { return Triggered; }
		set {
			if (onTriggeredCalled != null)
				onTriggeredCalled (value);
			Triggered = value;
		}
	}


	public int value = 0;
	public objectState parent;
	public objectState child;
	public Color col;

	void Start () {
		
	}


	public Color changeColor () {
		col = new Color (randomColorVal (), randomColorVal (), randomColorVal ());
		applyColor (col);
		return col;
	}

	public void changeColor (Color _col) {
		col = _col;
		applyColor (col);

	}

	float randomColorVal () {
		return UnityEngine.Random.Range (0f, 1f);
	}

	void applyColor (Color col) {
		try {
			GetComponent<MeshRenderer> ().material.color = col;
			transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().material.SetColor ("_MainColor", col);
		} catch {
			Debug.LogWarning ("could not set color for object: " + name);
		}
	}



	void Update () {
		if (child != null)
			Debug.DrawLine (transform.position, child.transform.position, triggered ? Color.red : Color.cyan, 1f);
	}
}
