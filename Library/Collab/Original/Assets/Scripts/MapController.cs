using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {
	public Queue <IEnumerator> bakeQueue;
	public GameObject room;

	void Start () {
		bakeQueue = new Queue<IEnumerator> ();
		generateRoom ('N', new Vector2 (0f, 0f));
		StartCoroutine (serveBakeQueue ());
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

	IEnumerator serveBakeQueue () {
		while (true) { // constanlty
			yield return new WaitUntil (() => { // wait until 
				return (bakeQueue.Count > 0); // there is comthing in the queue
			});
			// when there is:
			IEnumerator bake = bakeQueue.Dequeue (); // dequeue
			yield return StartCoroutine (bake); // and bake!
		}
	}


}
