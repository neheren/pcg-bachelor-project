using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {
	public Queue <IEnumerator> bakeQueue;
	public GameObject room;
	public int uniqueId;
	public float timeInCurrentRoom;
	public bool finalRoom;
	public string finalRoomName;
	public List<GameObject> spawnedRooms = new List<GameObject> ();
	public GameObject player;
	private int roomAValue;

	public IEnumerator removeRoomWhenReady (GameObject mesh) {
		yield return new WaitWhile (() => {
			return (mesh.GetComponent<slytMeshCombine> ().meshReference == null);
		});
		yield return new WaitForSeconds (6f);
		mesh.SetActive (false);
		mesh.GetComponent<slytMeshCombine> ().meshReference.SetActive (false);
	}



	public int roomAmount {
		get {
			return roomAValue;
		}
		set {
			for (int i = 0; i < spawnedRooms.Count; i++) {

				if (i < spawnedRooms.Count - 3) {
					if (spawnedRooms [i].GetComponent<slytMeshCombine> () != null) {
						if (spawnedRooms [i].GetComponent<slytMeshCombine> ().meshReference != null) {
							spawnedRooms [i].SetActive (false);
							spawnedRooms [i].GetComponent<slytMeshCombine> ().meshReference.SetActive (false);
						} else {
							StartCoroutine (removeRoomWhenReady (spawnedRooms [i]));
						}
					} else {
						spawnedRooms [i].SetActive (false);
					}
				}
			}
			//BY DISTANCE:
			//					if (Vector3.Distance (spawnedRooms [i].transform.position, player.transform.position) > 50) {
			//						spawnedRooms [i].SetActive (false);
			//						spawnedRooms [i].GetComponent<slytMeshCombine> ().meshReference.SetActive (false);
			//					}
					
//			} catch {
//				Debug.LogError ("could not disable stuff");
//			}

			roomAValue = value;
			if (roomAValue == 5) {
				desireCanvas.SetActive (true);
			}
		}
	}

	public PlayerBehavouir playerBehavouir;
	StoryController storyController;

	connect slytterSheets;
	List<string> gatheredData = new List<string> ();
	GameObject desireCanvas;

	void Start () {
		spawnedRooms.Add (GameObject.Find ("StartRoom"));
		DontDestroyOnLoad (this);
		player = GameObject.FindWithTag ("Player");
		playerBehavouir = GetComponent<PlayerBehavouir> ();
		finalRoom = false;
		desireCanvas = GameObject.FindWithTag ("desire");
		slytterSheets = GetComponent<connect> ();

		try {
			print ("using id from last scene");
			uniqueId = GameObject.FindWithTag ("id").GetComponent<idGen> ().uniqueId;
		} catch {
			Debug.LogError ("couldd not find id, generating new.");
			uniqueId = Random.Range (0, 1000000);
		}

		bakeQueue = new Queue<IEnumerator> ();
		StartCoroutine (serveBakeQueue ());
		storyController = GameObject.FindWithTag ("storyController").GetComponent<StoryController> ();
	}


	public GameObject generateRoom (char direction, Vector2 spawnPosition, int id) {
		sendRoomData ();
//		print ("generating " + direction + " room at " + spawnPosition);
		GameObject spawnedRoom = Instantiate (room, new Vector3 (spawnPosition.x, 0, spawnPosition.y), Quaternion.Euler (0, 0, 0));
		spawnedRoom.name = "Room " + direction + id;
		spawnedRoom.GetComponent<generateRoom> ().StartGeneration (direction);
		spawnedRooms.Add (spawnedRoom); //!!
		return spawnedRoom;
	}

	void sendRoomData () {
		gatheredData.Add (uniqueId.ToString ());
		gatheredData.Add (timeInCurrentRoom.ToString ());
		gatheredData.Add ("prototype: " + playerBehavouir.prototype);
		gatheredData.Add ("puzzles completed: " + playerBehavouir.PuzzleCompleted);
		gatheredData.Add ("story completed: " + playerBehavouir.StoriesCompleted);
		gatheredData.Add ("comp. completed: " + playerBehavouir.CompCompleted);
		gatheredData.Add ("amount of rooms: " + (spawnedRooms.Count + 1));
		gatheredData.Add ("puzzle chain size: " + playerBehavouir.puzzleChainSize.ToString ());
		StartCoroutine (slytterSheets.connectToSlytterSheets (slytterSheets.urlEncodeString (gatheredData)));
		gatheredData = new	List<string> ();
		timeInCurrentRoom = 0;
	}


	void Update () {
		timeInCurrentRoom += Time.deltaTime;
	}


	IEnumerator serveBakeQueue () {
		while (true) { // constanlty
			bakeStatus = "Waiting for bake in queue";
			yield return new WaitUntil (() => { // wait until 
				return (bakeQueue.Count > 0); // there is comthing in the queue
			});
			// when there is:
			IEnumerator bake = bakeQueue.Dequeue (); // dequeue
			bakeStatus = "Bake dequeued and runnning. " + bakeQueue.Count + " left in bake queue.";
			yield return StartCoroutine (bake); // and bake!
			bakeStatus = "Done baking.";
		}
	}

	public string bakeStatus;


	/*void OnGUI () {
		if (GUI.Button (new Rect (10, 10, 100, 30), "new room")) {
			generateRoom ('N', new Vector2 (0f, 0f), 0);
		}
		GUI.Box (new Rect (10, 50, 400, 30), "slyt-baker: " + bakeStatus);
	}*/


}
