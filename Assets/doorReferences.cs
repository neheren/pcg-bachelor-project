using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorReferences : MonoBehaviour {
	public List<TriggerRoomGeneration> doors;
	public Vector3 middlePosition;
	public List<GameObject> sorroundingRooms;
	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds (1f);
		middlePosition = new Vector3 (0, 0, 0);
//		doors = new List<TriggerRoomGeneration> ();
		DestroyCube[] doors = GetComponentsInChildren<DestroyCube> ();
		Vector3 collectedPosition = new Vector3 (0, 0, 0);
		foreach (var item in doors) {
			sorroundingRooms.Add (item.gameObject.GetComponentInChildren<TriggerRoomGeneration> ().generatedRoom);
			if (item.gameObject.GetComponentInChildren<TriggerRoomGeneration> ().used) {
				collectedPosition += item.transform.position;
			}
		}
		collectedPosition += transform.position;
		middlePosition = collectedPosition / 4;


//		int xMissing = 2;
//		int zMissing = 2;
//		foreach (var item in doors) {
//			char direction = item.gameObject.GetComponentInChildren<TriggerRoomGeneration> ().direction;
//			if (item.gameObject.GetComponentInChildren<TriggerRoomGeneration> ().used) {
//				print ("direction " + direction);
//				if (direction == 'N') {
//					xMissing--;
//					middlePosition += new Vector3 (item.transform.position.x, 0f, 0f);
//				} else if (direction == 'S') {
//					xMissing--;
//					middlePosition += new Vector3 (item.transform.position.x, 0f, 0f);
//				} else if (direction == 'E') {
//					middlePosition += new Vector3 (0f, 0f, item.transform.position.z);
//					zMissing--;
//				} else if (direction == 'W') {
//					middlePosition += new Vector3 (0f, 0f, item.transform.position.z);
//					zMissing--;
//				}
//			}
//
//		}
//		if (zMissing == 1) {
//			middlePosition += new Vector3 (0f, 0f, transform.position.z);
//			print ("missing one z");
//			middlePosition = new Vector3 (middlePosition.x / 2, middlePosition.y, middlePosition.z / 2);
//
//		}
//		if (xMissing == 1) {
//			middlePosition += new Vector3 (transform.position.x, 0f, 0f);
//			print ("missing one x");
//			middlePosition = new Vector3 (middlePosition.x / 4, middlePosition.y, middlePosition.z / 2);
//		}
////		middlePosition += transform.position;
//		middlePosition /= 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
