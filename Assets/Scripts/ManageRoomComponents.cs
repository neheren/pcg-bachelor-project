using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ManageRoomComponents : MonoBehaviour {

	public List <string> storySpawnables;
	public List<GameObject> centerSpawnPlacements;
	public List<GameObject> wallSpawnPlacements;
	int originalSpawnablesLength;
	MapController mapController;
	PlayerBehavouir playerBehavouir;

	void Start () {
		mapController = GameObject.FindWithTag ("GameController").GetComponent<MapController> ();
		playerBehavouir = GameObject.FindWithTag ("GameController").GetComponent<PlayerBehavouir> ();
		int storyAmount = playerBehavouir.calculateStoryAmount ();
		//	print (storyAmount);
		for (int i = 0; i < storyAmount; i++) {
			storySpawnables.Add ("StoryObject");
		}
		//	print (storySpawnables.Count);
		originalSpawnablesLength = storySpawnables.Count;
		StartCoroutine (waitForSpawnPlacements ());
	}

	IEnumerator waitForSpawnPlacements () {
		bool success = true;
//		print (name + " waiting to spawn " + spawnables.Count + " elements");
		yield return new WaitUntil (() => { 
			return (centerSpawnPlacements.Count != 0);
		});
		centerSpawnPlacements = Scramble (centerSpawnPlacements); 
		wallSpawnPlacements = Scramble (wallSpawnPlacements);
		centerSpawnPlacements.AddRange (wallSpawnPlacements);


		puzzleGenerator2 puzzleGen = GetComponent<puzzleGenerator2> ();
		puzzleGen.Start ();

		int chainSize = playerBehavouir.chainSize ();
		if (chainSize > 0) {
			puzzleGen.generateSubWithSkew (chainSize, "LOCK"); // here
		}


		
		List<string> compElements = new List<string> ();
		print (playerBehavouir.calculateCompDifficulty ());
		compElements.Add ("CompObject" + playerBehavouir.calculateCompDifficulty ()); 

		puzzleGen.objectsToSpawn.AddRange (storySpawnables);
		List <string> totalObjectsToSpawn = puzzleGen.objectsToSpawn;
		totalObjectsToSpawn.AddRange (compElements);
		List<GameObject> instantiatedGameObjects = new List<GameObject> ();

		for (int i = 0; i < totalObjectsToSpawn.Count; i++) {
			if (i >= centerSpawnPlacements.Count) {
				success = false;
				Debug.LogError ("Not enough spawn placements! Please implement more in generateRoom");
				break;
			}

			GameObject instantiatedGameobject;
			if (totalObjectsToSpawn [i] != "LOCK") {
				try {
					instantiatedGameobject = Instantiate ((GameObject)Resources.Load (totalObjectsToSpawn [i]), centerSpawnPlacements [i].transform.position, Quaternion.Euler (Vector3.zero), this.transform);
					Vector3 tempPos = instantiatedGameobject.transform.position;
					float heightSkew = 0.5f;
					instantiatedGameobject.transform.position = new Vector3 (tempPos.x, heightSkew, tempPos.z);
				} catch {
					instantiatedGameobject = null;
					Debug.Log ("Error in instantiate, resources.load is prob. null");
				}
			} else {
				instantiatedGameobject = null;
				foreach (var item in GameObject.FindGameObjectsWithTag ("door")) {
					if (item.GetComponent<TriggerRoomGeneration> () != null && !item.GetComponent<TriggerRoomGeneration> ().used)
						instantiatedGameobject = item;
				}
			}

			instantiatedGameObjects.Add (instantiatedGameobject);
			if (i - 1 >= 0 && instantiatedGameObjects [i - 1].GetComponent<objectState> () != null) {
				try {
					instantiatedGameObjects [i].GetComponent<objectState> ().parent = instantiatedGameObjects [i - 1].GetComponent<objectState> ();
					instantiatedGameObjects [i - 1].GetComponent<objectState> ().child = instantiatedGameObjects [i].GetComponent<objectState> ();
				} catch {
//					print ("could not find parent for " + instantiatedGameObjects [i] + " or child for " + instantiatedGameObjects [i - 1]);
				}
			}
		}
		foreach (var item in centerSpawnPlacements) {
			Destroy (item);
		}
	}

	List<GameObject> Scramble (List<GameObject> input) {
		return input.OrderBy (x => Random.value).ToList ();
	}
}
