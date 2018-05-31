using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectToDoor : MonoBehaviour {
	public TriggerRoomGeneration doorHook;
	public CanvasController CanvasController;

	IEnumerator Start () {

		CanvasController = GameObject.Find ("TextCanvas").GetComponent<CanvasController> (); 

		//		print ("Waiting for storyobjects to be ready");
//		yield return new WaitUntil (() => {
//			return GetComponentsInChildren<GetString> ().Length != 0;
//		});
		//		print ("storyobjects ready");

		yield return new WaitForSeconds (1.1f);

		TriggerRoomGeneration[] doors = transform.parent.GetComponentsInChildren<TriggerRoomGeneration> ();

		foreach (var door in doors) { // finding the first door withour connection
			if (!door.GetComponent<TriggerRoomGeneration> ().hasBeenConnected) {
//				print ("found story controller door hook " + door.direction);
				door.setColor (Color.black);
				door.otherDoor.setColor (Color.black);
				door.hasBeenConnected = true;
				door.otherDoor.hasBeenConnected = true;
				door.connectedToComp = true;
				door.otherDoor.connectedToComp = true;
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
		StartCoroutine (CanvasController.BlackDoorText ());
	}

}
