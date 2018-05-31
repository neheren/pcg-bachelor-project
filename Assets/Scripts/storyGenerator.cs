using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storyGenerator : MonoBehaviour {
	StoryController storyController;
	public TriggerRoomGeneration doorHook;
	// Use this for initialization
	IEnumerator Start () {
		StoryController storyController = GameObject.FindWithTag ("storyController").GetComponent<StoryController> ();
//		print ("Waiting for storyobjects to be ready");
		yield return new WaitUntil (() => {
			return GetComponentsInChildren<GetString> ().Length != 0;
		});
//		print ("storyobjects ready");

		storyController.newestRoomStoryUnread = GetComponentsInChildren<GetString> ().Length;

		yield return new WaitForSeconds (1);

		TriggerRoomGeneration[] doors = gameObject.GetComponentsInChildren<TriggerRoomGeneration> ();

		foreach (var door in doors) { // finding the first door withour connection
			if (!door.GetComponent<TriggerRoomGeneration> ().hasBeenConnected) {
//				print ("found story controller door hook " + door.direction);
				door.setColor (Color.yellow);
				door.otherDoor.setColor (Color.yellow);
				door.hasBeenConnected = true;
				door.otherDoor.hasBeenConnected = true;
				door.connectedToStory = true;
				door.otherDoor.connectedToStory = true;
				doorHook = door;
				break;
			}
		}
	}

	public void unlockDoor () {
		doorHook.colorUnlock (Color.white);
		doorHook.otherDoor.colorUnlock (Color.white);
		doorHook.locked = false;
		doorHook.otherDoor.locked = false;
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
