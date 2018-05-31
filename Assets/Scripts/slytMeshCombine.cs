using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slytMeshCombine : MonoBehaviour {
	MB3_MeshBaker meshbaker;
	public GameObject meshReference;
	// Use this for initialization
	void Start () {
		meshbaker = GetComponent<MB3_MeshBaker> ();
	}

	public IEnumerator run (List<GameObject> MeshCombine, string roomName) {
		yield return new WaitForSeconds (2);
		meshbaker.transform.parent = this.transform;
		string combiner = "CombinedMesh-" + roomName + "-mesh";
		yield return StartCoroutine (recusiveCombine (MeshCombine, combiner, 6, 1, roomName));
		yield return new WaitForSeconds (0.5f);
		yield return null;
	}


	IEnumerator recusiveCombine (List <GameObject>  meshes, string combiner, int steps, int iteration, string roomName) {
		List <GameObject> returnMeshes = new List<GameObject> ();
		GameObject parent = new GameObject (combiner + " merge iteration " + iteration);

		for (int i = 0; i < meshes.Count; i += steps) {
			List <GameObject> stepMeshes = new List<GameObject> ();
			for (int j = 0; j < steps; j++) {
				if (i + j < meshes.Count) {
					stepMeshes.Add (meshes [i + j]);
				} else {
					break;
				}
			}
			yield return StartCoroutine (combineMesh (stepMeshes.ToArray (), i, combiner, parent.transform, result => {
				returnMeshes.Add (result);
			}));
		}
		Transform lastIteration = GameObject.Find (combiner + " merge iteration " + iteration).transform;
		List <GameObject> lastIterationObjects = new List<GameObject> ();

		for (int child = 0; child < lastIteration.childCount; child++) {
			lastIterationObjects.Add (lastIteration.GetChild (child).gameObject);
		}
		if (lastIterationObjects.Count > 1)
			yield return StartCoroutine (recusiveCombine (lastIterationObjects, combiner, steps, iteration + 1, roomName));

		Destroy (lastIteration.gameObject);

		GameObject combined = GameObject.Find (combiner);
		combined.transform.GetChild (0).gameObject.AddComponent<MeshCollider> ().convex = false;
		if (iteration == 1) {
			meshReference = GameObject.Find (combiner);
			GameObject.Find (combiner).name = "Slyt Combined " + roomName;
		}

		yield return gameObject;
	}

	IEnumerator combineMesh (GameObject[] meshes, int index, string combiner, Transform parent, System.Action<GameObject> action) {
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

		GameObject meshObjectChild = GameObject.Find (combiner).transform.GetChild (0).gameObject;
		GameObject generatedObj = GameObject.Instantiate (meshObjectChild);
		generatedObj.GetComponent<MeshFilter> ().mesh = meshObjectChild.GetComponent<MeshFilter> ().mesh;
		generatedObj.AddComponent<MeshCollider> ().convex = false;
		generatedObj.name = meshes.Length + " combined meshes";
		generatedObj.transform.parent = parent;
		meshbaker.DestroyMesh ();
		action (generatedObj);

	}

}
