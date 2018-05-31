using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class StoryEndCol : MonoBehaviour {

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController Controller;

    public GameObject EndStoryCanvas;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;

	
	void Start () {

        Controller = GameObject.Find("FPSController").GetComponent<FirstPersonController>();

        EndStoryCanvas = GameObject.Find("EndStoryCanvas");
        Text1 = GameObject.Find("Text1");
        Text2 = GameObject.Find("Text2");
        Text3 = GameObject.Find("Text3");

        EndStoryCanvas.SetActive(false);
        Text1.SetActive(false);
        Text2.SetActive(false);
        Text3.SetActive(false);


    }



     void OnTriggerEnter(Collider other) {
         if (other.transform.gameObject.tag == "Player") {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Controller.m_MouseLook.XSensitivity = 0;
            Controller.m_MouseLook.YSensitivity = 0;
            Controller.m_WalkSpeed = 0;
            Controller.m_RunSpeed = 0;
            Controller.m_JumpSpeed = 0;
            Controller.m_MouseLook.SetCursorLock(false);

            EndStoryCanvas.SetActive(true);
            Text1.SetActive(true);

        }

    }

    public void LoadText2() {

        Text1.SetActive(false);
        Text2.SetActive(true);

    }


    public void LoadText3() {

        Text2.SetActive(false);
        Text3.SetActive(true);
    }


    public void LastButton() {

        EndStoryCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        Controller.m_MouseLook.SetCursorLock(true);
        Controller.m_MouseLook.XSensitivity = 2;
        Controller.m_MouseLook.YSensitivity = 2;
        Controller.m_WalkSpeed = 5;
        Controller.m_RunSpeed = 5;
        Controller.m_JumpSpeed = 7;

    }

}
