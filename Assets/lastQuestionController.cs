using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;
using UnityEngine.UI;

public class lastQuestionController : MonoBehaviour {
	connect slytterSheets;
	DeterminePrototype prototype;
	MapController mapcontroller;
	// Use this for initialization
	void Start () {
		slytterSheets = GetComponent<connect> ();
		prototype = GetComponent<DeterminePrototype> ();
		mapcontroller = GameObject.FindWithTag ("GameController").GetComponent<MapController> ();
	}
	
	// Update is called once per frame
	public void submit () {
		List<string> answers = new List<string> ();
		try {
			answers.Add ("Last query from id: " + mapcontroller.uniqueId);
		} catch {
			Debug.LogError ("No map controller");
		}
		ToggleGroup toggle = GetComponentInChildren<ToggleGroup> ();
		if (toggle != null) {
			for (int j = 0; j < toggle.transform.childCount; j++) {
				Toggle answer = toggle.transform.GetChild (j).GetComponentInChildren<Toggle> ();
				Text question = toggle.transform.GetChild (j).GetComponentInChildren<Text> ();
				if (answer.isOn) {
					print ("continuation desire: " + question.text);
					answers.Add ("continuation desire: " + question.text);
					StartCoroutine (slytterSheets.connectToSlytterSheets (slytterSheets.urlEncodeString (answers)));
					StartCoroutine (prototype.confirmPrototype (mapcontroller.playerBehavouir.prototype));
				}
			}
		}
	}

}
