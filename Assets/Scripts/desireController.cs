using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using MaterialUI;

public class desireController : MonoBehaviour {
	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController Controller;
	public string choice;
	public GameObject gun;
	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForEndOfFrame ();
		gameObject.SetActive (false);
	}

	void OnEnable () {
		GetComponentInChildren<ToggleGroup> ().SetAllTogglesOff ();
		GetComponentInChildren<Button> ().interactable = false;
		StartCoroutine (fadeIn ());
	}

	IEnumerator fadeIn () {
		yield return new WaitForSeconds (2f);
		Animation animation = GetComponent<Animation> ();
		animation.Play ("faceCanvasIn");
		yield return new WaitWhile (() => {
			return animation.isPlaying;
		});

		enableMouse (true);
		gun.GetComponent<animationController> ().allowShooting = false;
		

	}


	IEnumerator fadeOut () {
		Animation animation = GetComponent<Animation> ();

		animation.Play ("fadeCanvasOut");
		enableMouse (false);
		yield return new WaitWhile (() => {
			return animation.isPlaying;
		});
		gameObject.SetActive (false);
	}

	void enableMouse (bool enabled) {
		Controller = GameObject.Find ("FPSController").GetComponent<FirstPersonController> ();
		Cursor.lockState = (enabled) ? CursorLockMode.None : CursorLockMode.Locked;
		Cursor.visible = enabled;
		Controller.m_MouseLook.XSensitivity = (!enabled) ? 2 : 0;
		Controller.m_MouseLook.YSensitivity = (!enabled) ? 2 : 0;
		Controller.m_WalkSpeed = (!enabled) ? 5 : 0;
		Controller.m_RunSpeed = (!enabled) ? 5 : 0;
		Controller.m_JumpSpeed = (!enabled) ? 7 : 0;
		Controller.m_MouseLook.SetCursorLock (!enabled);
	}

	public void choseAnswer (string _choice) {
		choice = _choice;
		Button[] ui = GetComponentsInChildren<Button> ();
		foreach (var item in ui) {
			item.interactable = true;
		}

	}

	public void sumbitDesire () {
		//	SelectionBoxConfig ui = GetComponentInChildren<SelectionBoxConfig> ();
		//	string selecteditem = ui.listItems [ui.currentSelection];
		//	print (selecteditem);
		connect slytterSheets = GameObject.FindWithTag ("GameController").GetComponent<connect> ();
		MapController mapController = GameObject.FindWithTag ("GameController").GetComponent<MapController> ();
		StartCoroutine (slytterSheets.connectToSlytterSheets (slytterSheets.urlEncodeString (new List<string> () {
			"id: " + mapController.uniqueId,
			"Room amount: " + mapController.roomAmount,
			"Continuation desire: " + choice,
		})));
		GetComponentInChildren<ToggleGroup> ().SetAllTogglesOff ();
		gun.GetComponent<animationController> ().allowShooting = true;
		StartCoroutine (fadeOut ());
	}
}
