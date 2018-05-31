using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompCubeHit : MonoBehaviour {

	float scalingFactor = 0.1f;
	float TimeScale = 0.8f;
	Vector3 InitialScale;
	Vector3 FinalScale;
	public bool readyToKill = false;

	public void Start () {

		FinalScale = new Vector3 (0.3f, 0.3f, 0.3f);
		InitialScale = new Vector3 (0, 0, 0);
	}

	public void IsHit () {
		print ("smageg");
		if (readyToKill == false) {
			print ("Cube er ramt!");
			this.transform.localScale = InitialScale;
			readyToKill = true;
			transform.parent.gameObject.GetComponent<CheckForDestroy> ().checkfordestroy ();
			StartCoroutine (LerpUp ());
		}
	}



	IEnumerator LerpUp () {
		float progress = 0;
		yield return new WaitForSeconds(0.8f);
		while (progress <= 1) {
			this.transform.localScale = Vector3.Lerp (InitialScale, FinalScale, progress);
			progress += Time.deltaTime * TimeScale;
			yield return null;
		}
		readyToKill = false;
		print ("Så er jeg fuld igen");
	}


}


