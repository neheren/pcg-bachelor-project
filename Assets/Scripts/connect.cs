using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connect : MonoBehaviour {

	public string url = "slytter.tk:5000/insert?";




	public IEnumerator connectToSlytterSheets (string dataString) {
		WWW www = new WWW (url + dataString);
		yield return www;
		string statusCode = "";
		try {
			www.responseHeaders.TryGetValue ("Status", out statusCode);
		} catch {
			Debug.LogError ("HTTP Request failed.");
		}
		if (statusCode == "HTTP/1.1 200 OK")
			print ("Status OK!");
		else
			Debug.LogError ("HTTP Request failed.");
		yield return new WaitForSeconds (0);
	}

	public string urlEncodeString (List<string> data) {
		string encodedString = "";
		for (int i = 0; i < data.Count; i++) {
			encodedString += "data" + i + "=" + data [i] + "&";
		}
		return encodedString;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
