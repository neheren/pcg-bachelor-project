using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slyt_Baker : MonoBehaviour {
	MB3_MeshBaker meshbaker;
	string stringId;

	void Start () {
		meshbaker = GetComponent<MB3_MeshBaker> ();
		stringId = gameObject.name + Random.Range (0, 100000); // unique name thing
	}

	public IEnumerator generateRoom (List<GameObject> gameObjectsToCombine, string roomId) {
		bakeStatus = "Waiting for animation";
		yield return new WaitForSeconds (2);
		meshbaker.transform.parent = this.transform;

		yield return StartCoroutine (recusiveCombine (gameObjectsToCombine, roomId, 8, 1));
		//	yield return StartCoroutine (combineMesh (gameObjectsToCombine.ToArray (), 0, roomId, this.transform, res => {
		//		print (res);
		//	}));
		//		meshbaker.DestroyMesh ();
		yield return new WaitForSeconds (0.5f);
		//yield return StartCoroutine (recusiveCombine (wallMeshCombine, 8, 1));
		bakeStatus = "Walls in room " + stringId + "done";
	}












	IEnumerator recusiveCombine (List <GameObject>  meshes, string threadId, int steps, int iteration) {
		List <GameObject> returnMeshes = new List<GameObject> ();
		GameObject parent = new GameObject (threadId + " merge iteration " + iteration);

		for (int i = 0; i < meshes.Count; i += steps) {
			List <GameObject> stepMeshes = new List<GameObject> ();
			for (int j = 0; j < steps; j++) {
				if (i + j < meshes.Count) {
					stepMeshes.Add (meshes [i + j]);
				} else {
					print ("breaking");
					break;
				}
			}
			bakeStatus = "Starting iteration " + iteration;
			yield return StartCoroutine (combineMesh (stepMeshes.ToArray (), iteration, threadId, parent.transform, result => {
				bakeStatus = "Iteration done";
				returnMeshes.Add (result);
			}));
		}

		print (threadId + " merge iteration " + iteration);

		Transform lastIteration = GameObject.Find (threadId + " merge iteration " + iteration).transform;
		List <GameObject> lastIterationObjects = new List<GameObject> ();

		for (int child = 0; child < lastIteration.childCount; child++) {
			lastIterationObjects.Add (lastIteration.GetChild (child).gameObject);
		}
		if (lastIterationObjects.Count > 1)
			StartCoroutine (recusiveCombine (lastIterationObjects, threadId, steps, iteration + 1));
		yield return gameObject;
	}









	IEnumerator combineMesh (GameObject[] meshes, int iteration, string roomId, Transform parent, System.Action<GameObject> action) {
		meshbaker.AddDeleteGameObjects (meshes, null, false);
		meshbaker.Apply ();
		yield return new WaitForEndOfFrame ();

		int deleteSteps = 10;
		for (int i = 0; i < meshes.Length; i += deleteSteps) {
			for (int j = 0; j < deleteSteps; j++) {
				if (i + j < meshes.Length) {
					Destroy (meshes [i + j]);
				}
			}
			yield return new WaitForEndOfFrame ();
		}

		//room-N0 merge iteration 1
		// CombinedMesh-room-N0-mesh
		string searchName = "CombinedMesh-" + roomId + "-mesh";
		print (searchName);
		GameObject meshObject = GameObject.Find (searchName);
		//meshObject.name = threadId;

		GameObject meshObjectChild = meshObject.transform.GetChild (0).gameObject;
		GameObject generatedObj = GameObject.Instantiate (meshObjectChild);
		generatedObj.GetComponent<MeshFilter> ().mesh = meshObjectChild.GetComponent<MeshFilter> ().mesh;
		generatedObj.AddComponent<MeshCollider> ().convex = false;

		Destroy (meshObject);
		generatedObj.name = meshes.Length + " combined meshes";
		generatedObj.transform.parent = parent;
		meshbaker.DestroyMesh ();
		action (generatedObj);

	}



	public string bakeStatus;

	void OnGUI () {
		GUI.Box (new Rect (10, 50, 400, 30), "slyt-baker: " + bakeStatus);
	}


}
