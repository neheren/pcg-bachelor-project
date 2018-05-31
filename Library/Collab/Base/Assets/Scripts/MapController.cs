using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

	public GameObject room;
	//public Vector2[] spawnPositions;

	// Use this for initialization
	void Start () {
		//spawnPositions = new Vector2[3] { new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0) };
		generateRoom ('N', new Vector2 (0f, 0f));
	}

	void OnGUI () {
		if (GUI.Button (new Rect (10, 10, 100, 30), "new room")) {
			generateRoom ('N', new Vector2 (0f, 0f));
		}
	}

	public void generateRoom (char direction, Vector2 spawnPosition) {
		print ("generating " + direction + " room at " + spawnPosition);
		GameObject spawnedRoom = Instantiate (room, new Vector3 (spawnPosition.x, 0, spawnPosition.y), Quaternion.Euler (0, 0, 0));
		spawnedRoom.GetComponent<generateRoom> ().StartGeneration (direction);
	}




}
