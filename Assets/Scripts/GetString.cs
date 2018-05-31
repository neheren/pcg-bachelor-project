using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

public class GetString : MonoBehaviour {

	string sameText;
    public CanvasController CanvasController;
    public StoryController StoryController;
	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController Controller;
	public Transform PlayerPos;
	bool isRead = false;
    public bool Reading = false;
    

	void Start () {

        CanvasController = GameObject.Find("TextCanvas").GetComponent<CanvasController>();
        StoryController = GameObject.Find ("StoryController").GetComponent<StoryController> ();
		Controller = GameObject.Find ("FPSController").GetComponent<FirstPersonController> ();
		PlayerPos = GameObject.FindWithTag ("Player").transform;
		Controller.tag = "Player";

		// det er en tænker...
		//GameObject[] rooms = GameObject.FindGameObjectsWithTag ("room");
		//this.transform.LookAt (rooms [rooms.Length - 1].GetComponent<Collider> ().bounds.center);
        
	}

	void OnTriggerStay (Collider other) {
        
		if (other.transform.gameObject.tag == "Player" && Input.GetKeyDown (KeyCode.E)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Controller.m_MouseLook.XSensitivity = 0;
			Controller.m_MouseLook.YSensitivity = 0;
			Controller.m_WalkSpeed = 0;
			Controller.m_RunSpeed = 0;
			Controller.m_JumpSpeed = 0;
			Controller.m_MouseLook.SetCursorLock (false);
            Reading = true;


			if (isRead == false) {
				StoryController.newestRoomStoryUnread--;
				if (StoryController.newestRoomStoryUnread <= 0) {
					print ("story door unlocked.");
                    try {

                        GetComponentInParent<storyGenerator> ().unlockDoor ();

                    }
                    catch {
                        print("no door found");
                    }

				}
				sameText = StoryController.getStory ();
				isRead = true;
			} 
			transform.GetChild (0).gameObject.SetActive (true);
			transform.GetChild (0).gameObject.GetComponentInChildren<Text> ().text = sameText;
			GetComponent<AudioSource> ().Play ();

		}
	}


    private void Update() {
        if (Reading == true && Input.GetKeyDown (KeyCode.Escape)) {


            HideText();

        }
    }

    public void HideText () {
		transform.GetChild (0).gameObject.SetActive (false);
        Reading = false;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
		Controller.m_MouseLook.SetCursorLock (true);
		Controller.m_MouseLook.XSensitivity = 2;
		Controller.m_MouseLook.YSensitivity = 2;
		Controller.m_WalkSpeed = 5;
		Controller.m_RunSpeed = 5;
		Controller.m_JumpSpeed = 7;

        if (StoryController.newestRoomStoryUnread <= 0) {
            StartCoroutine(CanvasController.YellowDoorText());
        }
        //Controller.m_MouseLook.lockCursor = true;

    }

   

}
