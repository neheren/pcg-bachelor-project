using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

	public GameObject room;
	public Vector2[] spawnPositions;

	// Use this for initialization
	void Start () {
		spawnPositions = new Vector2[3] { new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0) };
		generateRoom (0);
	}

	void OnGUI () {
		if (GUI.Button (new Rect (10, 10, 100, 30), "new room")) {
			generateRoom (0);
		}
	}

	public void generateRoom (int direction) {
		Instantiate (room, new Vector3 (spawnPositions [direction].x, 0, spawnPositions [direction].y), Quaternion.Euler (0, 0, 0));
	}

}
