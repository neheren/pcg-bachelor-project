using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour {

	public int collected = 0;
	public GameObject playerInRoom;


	float time = 0f;

	void Update () {
		
//		time += Time.deltaTime;
//		if (time > 0.3f) {
//			GameObject[] rooms = GameObject.FindGameObjectsWithTag ("room");
//			float shortestDistance = 10000f;
//			int roomsWithShortestDistance = 0;
//			for (int i = 0; i < rooms.Length; i++) {
//				float dist = Vector3.Distance (transform.position, rooms [i].GetComponent<doorReferences> ().middlePosition);
//				if (dist < shortestDistance) {
//					shortestDistance = dist;
//					roomsWithShortestDistance = i;
//				}
//			}
//			playerInRoom = rooms [roomsWithShortestDistance];
//			
//			Debug.DrawLine (transform.position, rooms [roomsWithShortestDistance].GetComponent<doorReferences> ().middlePosition, Color.blue, 1f);	
//			
//			timeSinceSpawn += Time.deltaTime;
//			time = 0;
//		}
	}


	void OnControllerColliderHit (ControllerColliderHit hit) {
		if (hit.transform.tag == "door") {
			hit.gameObject.GetComponent<TriggerRoomGeneration> ().generateRoomHere ();
		}
		if (hit.transform.tag == "objective") {
			Destroy (hit.transform.gameObject);
			collected++;
		}
		if (hit.transform.tag == "pressure_plate") {
//			print ("hit plate");
			hit.transform.gameObject.GetComponent<pressureLogic> ().trigger ();
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
