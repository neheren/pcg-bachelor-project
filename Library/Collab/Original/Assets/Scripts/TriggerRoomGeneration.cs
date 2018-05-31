using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoomGeneration : MonoBehaviour {

	public char direction;
	public int id;
	public bool used = false;
	public bool locked = false;
	public Vector2 position;
	public bool letsGenerate = false;
	public bool hasBeenConnected = false;
	bool runOnce = false;
	public bool animationPlayed = false;
	objectState state;
	public TriggerRoomGeneration otherDoor;
	MapController mapController;
	// Use this for initialization
	bool connectedToPuzzle;

	void Init () {
		used = false;
	}

	void Start () {
		mapController = GameObject.FindWithTag ("GameController").GetComponent<MapController> ();
		int id = mapController.roomAmount;
		connectedToPuzzle = false;
		locked = true;
		setColor (Color.grey);
		state = GetComponent<objectState> ();
		if (state.child != null) {
			print ("Door has been connected to puzzle");
		}
		StartCoroutine (connectToPuzzle ());
	}

	IEnumerator connectToPuzzle () {
		yield return new WaitUntil (() => {
			return state.child != null;
		});
		print ("Door has been connected to puzzle");
		setColor (Color.red);
		yield return new WaitUntil (() => {
			return otherDoor != null;
		});
		connectedToPuzzle = true;
		otherDoor.setColor (Color.red);
		hasBeenConnected = true;
		otherDoor.hasBeenConnected = true;
	}

	public void setColor (Color col) {
		transform.GetChild (1).gameObject.GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", col);
		transform.GetChild (2).gameObject.GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", col);
	}

	// Update is called once per frame
	void Update () {
		if (!runOnce && state.triggered) {
			if (connectedToPuzzle) {
				GameObject.FindWithTag ("GameController").GetComponent<MapController> ().thisPuzzleCompleted = true;
				print ("PUZZLE UNLOCKED");
			}
			locked = false;
			otherDoor.locked = false;
			setColor (Color.white);
			otherDoor.setColor (Color.white);
			GetComponent<AudioSource> ().Play ();
			runOnce = true;
		}
		if (letsGenerate) {
			generateRoomHere ();
			letsGenerate = false;
		}
		if (!state.triggered && connectedToPuzzle == true) {
			locked = true;
			setColor (Color.red);

			otherDoor.setColor (Color.red);
			otherDoor.locked = true;
			runOnce = false;
		}
	}

	public bool openOnce = true;

	void openOtherDoorOnce () {
		print ("opening door");
		if (openOnce) {
			if (connectedToPuzzle) {
				mapController.PuzzleCompleted++;
			}
			openOnce = false;
			otherDoor.generateRoomHere ();
		}
	}

	public void generateRoomHere () {
		if (!locked) {
			otherDoor.locked = false;
			openOtherDoorOnce ();
			if (!animationPlayed) {
				GetComponent<Animation> ().Play ();
				animationPlayed = true;
			}
			if (!used) {
				used = true;
				MapController mapController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<MapController> ();
				mapController.roomAmount++;
				mapController.generateRoom (direction, position, id + 1);
			}
		}
	}

	bool checkCollide = true;

	void OnCollisionEnter (Collision col) {
		if (checkCollide && col.transform.tag == "door") {
			checkCollide = false;
			otherDoor = col.gameObject.GetComponent<TriggerRoomGeneration> ();
//			print ("door hit door!");
		}
	}
}