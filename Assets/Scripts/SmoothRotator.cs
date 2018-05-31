using UnityEngine;
using System.Collections;

public class SmoothRotator : MonoBehaviour {

	void Start () {
		//GetComponentInChildren<MeshRenderer> ().material.SetColor ("_EmissionColor", Color.red);
	}

	public void scramble () {
		roundAngle = (float)Random.Range (0, 4) * 90;
		transform.localRotation = Quaternion.Euler (0f, roundAngle, 0f);
	}

	bool rotating = false;
	float roundAngle;

	public IEnumerator Rotate (Vector3 axis, float angle, float duration = 1.0f) {
		roundAngle += angle;
		yield return new WaitWhile (() => rotating);
		rotating = true;

		Quaternion from = transform.rotation;
		Quaternion to = transform.rotation;
		to *= Quaternion.Euler (axis * angle);

		float elapsed = 0.0f;
		while (elapsed < duration) {
			transform.rotation = Quaternion.Slerp (from, to, elapsed / duration);
			elapsed += Time.deltaTime;
			yield return null;
		}
		transform.rotation = to;
		rotating = false;

	}




}