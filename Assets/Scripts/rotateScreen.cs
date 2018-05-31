using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateScreen : MonoBehaviour {

	Vector3[] directions = new Vector3[] {
		new Vector3 (0, 0, 1),
		new Vector3 (1, 0, 0),
		new Vector3 (0, 0, -1),
		new Vector3 (-1, 0, 0),
	};
	Vector3 ySkew = new Vector3 (0, 1.5f, 0);

	IEnumerator Start () {
		RaycastHit hit;
		Vector3 pointOfLongestDistance = new Vector3 ();
		float longestDistance = 0;
		yield return new WaitForSeconds (1f);
		for (int a = 0; a < 4; a += 1) {
			Vector3 dir = directions [a];

			if (Physics.Raycast (transform.position + ySkew, dir * 20f, out hit)) {
				Debug.DrawLine (transform.position + ySkew, hit.point, Color.yellow, 1000f);
				if (Vector3.Distance (transform.position + ySkew, hit.point) > longestDistance) {
					longestDistance = Vector3.Distance (transform.position + ySkew, hit.point);
					pointOfLongestDistance = hit.point;
				}
			}

		}

		Debug.DrawLine (transform.position + ySkew, pointOfLongestDistance, Color.red, 1000f);
		transform.LookAt (new Vector3 (pointOfLongestDistance.x, 0, pointOfLongestDistance.z));
		Debug.DrawRay (transform.position + ySkew, transform.forward * 10f, Color.blue, 1000f);
		// transform.rotation = Quaternion.Euler (new Vector3 (0, (transform.rotation.y * 360f) + 180f, 0));
	}

}
