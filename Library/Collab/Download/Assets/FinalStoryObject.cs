using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;
using UnityEngine.SceneManagement;

public class FinalStoryObject : MonoBehaviour {

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController Controller;
    public GameObject CanvasFader;

    void Start () {

        CanvasFader = GameObject.Find("CanvasFader");
        CanvasFader.SetActive(false);
        Controller = GameObject.Find("FPSController").GetComponent<FirstPersonController>();

    }

    private void OnTriggerStay(Collider other) {

        if (other.transform.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)) {



            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Controller.m_MouseLook.XSensitivity = 0;
            Controller.m_MouseLook.YSensitivity = 0;
            Controller.m_WalkSpeed = 0;
            Controller.m_RunSpeed = 0;
            Controller.m_JumpSpeed = 0;
            Controller.m_MouseLook.SetCursorLock(false);
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<AudioSource>().Play();



        }

    }


    public void ClickButton() {

        transform.GetChild(0).gameObject.SetActive(false);

        StartCoroutine(AnimationChangeScene());
       


    }

    IEnumerator AnimationChangeScene() {

        CanvasFader.SetActive(true);
        CanvasFader.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("FinalScene");
    }

}
