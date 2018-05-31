using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class EndStoryController : MonoBehaviour {

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController Controller;

    public GameObject FadeCanvas;


    private void Start() {

        Controller = GameObject.Find("FPSController").GetComponent<FirstPersonController>();

        FadeCanvas = GameObject.Find("FadeCanvas");
        FadeCanvas.SetActive(false);

    }
    private void OnTriggerEnter(Collider other) {


        if (other.transform.gameObject.tag == "Player") {

            Cursor.lockState = CursorLockMode.None;
           
            Controller.m_MouseLook.XSensitivity = 0;
            Controller.m_MouseLook.YSensitivity = 0;
            Controller.m_WalkSpeed = 0;
            Controller.m_RunSpeed = 0;
            Controller.m_JumpSpeed = 0;
            //Controller.m_MouseLook.SetCursorLock(false);

            FadeCanvas.SetActive(true);
            FadeCanvas.GetComponent<Animation>().Play();

            StartCoroutine(wait());

            
            
        }


    }


    IEnumerator wait() {

        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("StoryEnd");
    }
}
