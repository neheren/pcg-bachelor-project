using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}

	char direction;

	// Update is called once per frame
	void Update () {
		timeSinceSpawn += Time.deltaTime;
	}


	void OnControllerColliderHit (ControllerColliderHit hit) {
		if (hit.transform.tag == "door") {
			hit.gameObject.GetComponent<TriggerRoomGeneration> ().generateRoomHere ();
		}
	}

	float timeSinceSpawn = 0f;
	/*

	void OnTriggerEnter (Collider hit) {
		Transform child = hit.transform.GetChild (0).transform;
		if (hit.tag == "room" && !child.gameObject.activeInHierarchy) {
			child.gameObject.SetActive (true);
		}
	}

	void OnTriggerExit (Collider hit) {
		Transform child = hit.transform.GetChild (0).transform;

		if (hit.tag == "room" && child.gameObject.activeInHierarchy) {
			child.gameObject.SetActive (false);
		}	
	}*/

}
