using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class CollideWithPuzzleDeath : MonoBehaviour {

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController Controller;
    public GameObject FadeCanvas;


    void Start () {

        FadeCanvas = GameObject.Find("FadeCanvas");
        FadeCanvas.SetActive(false);
        Controller = GameObject.Find("FPSController").GetComponent<FirstPersonController>();

    }

        private void OnTriggerEnter(Collider other) {

        if (other.transform.gameObject.tag == "Player") {

            print("du er død nu.");
            Cursor.lockState = CursorLockMode.None;

            Controller.m_MouseLook.XSensitivity = 0;
            Controller.m_MouseLook.YSensitivity = 0;
            Controller.m_WalkSpeed = 0;
            Controller.m_RunSpeed = 0;
            Controller.m_JumpSpeed = 0;
            //Controller.m_MouseLook.SetCursorLock(false);

            FadeCanvas.SetActive(true);

            StartCoroutine(wait());


        }


    }

    IEnumerator wait() {

        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("FinalScene");
    }

}
