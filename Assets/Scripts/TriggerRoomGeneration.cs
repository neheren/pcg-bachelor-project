using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoomGeneration : MonoBehaviour {

	public CanvasController CanvasController;
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
	PlayerBehavouir pBehavauir;
	bool connectedToPuzzle;
	public bool connectedToStory;
	public bool connectedToComp;
	public GameObject generatedRoom;

	void Init () {
		used = false;
	}

	void Start () {
		CanvasController = GameObject.Find ("TextCanvas").GetComponent<CanvasController> ();
		mapController = GameObject.FindWithTag ("GameController").GetComponent<MapController> ();
		pBehavauir = GameObject.FindWithTag ("GameController").GetComponent<PlayerBehavouir> ();
		connectedToPuzzle = false;
//		connectedToStory = false;
		locked = true;
		setColor (Color.grey);
		state = GetComponent<objectState> ();
		StartCoroutine (connectToPuzzle ());
	}

	IEnumerator connectToPuzzle () {
		yield return new WaitUntil (() => {
			return state.child != null;
		});
//		print ("Door has been connected to puzzle");
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

	public void colorUnlock (Color col) {
		transform.GetChild (0).gameObject.GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", col);
	}

	bool runOnce2 = true;
	// Update is called once per frame
	void Update () {
		if (!runOnce && state.triggered) {
			locked = false;
			otherDoor.locked = false;
			colorUnlock (Color.white);
			otherDoor.colorUnlock (Color.white);
			GetComponent<AudioSource> ().Play ();
			runOnce = true;
			StartCoroutine (CanvasController.RedDoorText ());
			if (connectedToPuzzle && !used) {
				pBehavauir.PuzzleCompleted++;
			}

		}

		if (runOnce2 && !locked && !used && connectedToStory) {
			runOnce2 = false;
			pBehavauir.StoriesCompleted++;
		}

		if (runOnce2 && !locked && !used && connectedToComp) {
			runOnce2 = false;
			pBehavauir.CompCompleted++;
		}
		if (letsGenerate) {
			generateRoomHere ();
			letsGenerate = false;
		}
		if (!state.triggered && connectedToPuzzle == true) {
			//locked = true;
			//colorUnlock (Color.black);
			//otherDoor.colorUnlock (Color.black);
			//otherDoor.locked = true;
			//runOnce = false;
		}
	}

	public bool openOnce = true;

	void openOtherDoorOnce () {
		if (openOnce) {
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
				generatedRoom = GameObject.FindGameObjectWithTag ("GameController").GetComponent<MapController> ().generateRoom (direction, position, id + 1);
				generatedRoom.GetComponent<doorReferences> ().sorroundingRooms.Add (transform.parent.parent.gameObject);
				id = mapController.roomAmount++;
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