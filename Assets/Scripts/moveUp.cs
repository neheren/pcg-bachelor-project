using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUp : MonoBehaviour {

	public float pushDown = -50f;
	Vector3 original;
	public float speed = 0.01f;
	public float range = 10f;
	float lerpT = 0f;

	void OnEnable () {
		sStart ();
	}

	// Use this for initialization
	void sStart () {
		original = transform.position;
		pushDown -= Random.Range (0f, range);
		transform.position += new Vector3 (0, pushDown, 0);
	}
	
	// Update is called once per frame
	void Update () {

		if (pushDown < 0) {
			if (transform.position.y < original.y) {
				lerpT += speed;
				float newY = Mathf.LerpAngle (transform.position.y, original.y, lerpT);
				transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
			} else {
				Destroy (GetComponent<moveUp> ());
			}
		} else {
			if (transform.position.y > original.y) {
				lerpT += speed;
				float newY = Mathf.LerpAngle (transform.position.y, original.y, lerpT);
				transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
			} else {
				StartCoroutine (rotateAndDestroy ());
				Destroy (GetComponent<moveUp> ());
			}
		}
	}

	float waiter = 0f;

	IEnumerator rotateAndDestroy () {
		yield return new WaitForSeconds (waiter);
		waiter += 0.1f;
		GetComponent<Animation> ().Play ();
		yield return new WaitUntil (() => {
			return GetComponent<Animation> ().isPlaying;
		});
		Destroy (GetComponent<moveUp> ());
	}
}
