using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storyGenerator : MonoBehaviour {
	StoryController storyController;
	public TriggerRoomGeneration doorHook;
	// Use this for initialization
	IEnumerator Start () {
		storyController = GameObject.FindWithTag ("storyController").GetComponent<StoryController> ();
		storyController.newestRoomStoryUnread = 2;

		yield return new WaitForSeconds (5);

		TriggerRoomGeneration[] doors = gameObject.GetComponentsInChildren<TriggerRoomGeneration> ();

		foreach (var door in doors) { // finding the first door withour connection
			if (!door.GetComponent<TriggerRoomGeneration> ().hasBeenConnected) {
				print ("found story controller door hook " + door.direction);
				door.setColor (Color.yellow);
				door.otherDoor.setColor (Color.yellow);
				door.hasBeenConnected = true;
				door.otherDoor.hasBeenConnected = true;
				doorHook = door;
				break;
			}
		}
	}

	public void unlockDoor () {
		doorHook.setColor (Color.white);
		doorHook.otherDoor.setColor (Color.white);
		doorHook.locked = false;
		doorHook.otherDoor.locked = false;
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
