using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;
using UnityEngine.UI;

public class questionController : MonoBehaviour {

	// Use this for initialization
	public void submitAnswers () {
		List<string> answers = new List<string> ();
		answers.Add (GameObject.FindWithTag ("id").GetComponent<idGen> ().uniqueId.ToString ());

		for (int i = 0; i < transform.childCount; i++) {
			ToggleGroup toggle = transform.GetChild (i).GetComponentInChildren<ToggleGroup> ();
			textIdentifiet text = transform.GetChild (i).GetComponentInChildren<textIdentifiet> ();
			string realQuestion = transform.GetChild (i).GetComponentInChildren<Text> ().text;
			if (toggle != null) {
				// print ("question " + i + " has " + toggle.transform.childCount + " toggles.");
				for (int j = 0; j < toggle.transform.childCount; j++) {
					Toggle answer = toggle.transform.GetChild (j).GetComponentInChildren<Toggle> ();
					Text question = toggle.transform.GetChild (j).GetComponentInChildren<Text> ();
					if (answer.isOn) {
						answers.Add (realQuestion + ": " + question.text);
					}
				}
			} else if (text != null) {
				string question = text.transform.parent.GetComponentInChildren<Text> ().text;
				string answer = text.GetComponentInChildren<InputField> ().text;
				answers.Add (question + ": " + answer);
			}
		}
		connect slytterSheets = GetComponent<connect> ();
		StartCoroutine (slytterSheets.connectToSlytterSheets (slytterSheets.urlEncodeString (answers)));
	}

}
