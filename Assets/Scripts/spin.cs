using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour {
	float y;
	float t;

	void Start () {
		y = transform.position.y;
	}


	void FixedUpdate () {
		this.transform.Rotate (new Vector3 (0, 1f, 0));
		this.transform.position = new Vector3 (transform.position.x, y + Mathf.Sin (t) / 5f, transform.position.z);
		t += 0.05f;
	}
}
