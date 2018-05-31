using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeterminePrototype : MonoBehaviour {

	string baseUrl = "slytter.tk:4001/";


	public IEnumerator determinePrototype (System.Action<string> prototype) {
		using (WWW www = new WWW (baseUrl + "getPrototype")) {
			yield return www;
			if (www.error != null) {
				Debug.LogError ("Could not determine prototype: " + www.error);
			} else {
				prototype (www.text);
			}
		}
	}

	public IEnumerator confirmPrototype (string prototype) {
		using (WWW www = new WWW (baseUrl + "confirmPrototype/" + prototype)) {
			yield return www;
			if (www.error != null) {
				Debug.LogError ("Could not confirm prototype: " + www.error);
			} else {
				print ("Confirmed prototype w/ server response: " + www.text);
			}
		}
		yield return null;
	}
}
