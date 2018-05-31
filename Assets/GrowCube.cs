using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class GrowCube : MonoBehaviour {

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController Controller;
    float scalingFactor = 0.5f;
    float TimeScale = 0.1f;
    Vector3 InitialScale;
    Vector3 FinalScale;
    public GameObject FadeCanvas;

    void Start () {
        FadeCanvas = GameObject.Find("FadeCanvas");
        FadeCanvas.SetActive(false);

        Controller = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        FinalScale = new Vector3(20f, 20f, 20f);
        InitialScale = new Vector3(1.16f, 1.16f, 1.16f);
    }
	

    public IEnumerator GrowBigger() {

        float progress = 0;
        yield return new WaitForSeconds(3);
        print("Så er vi igang.");
        while (progress <= 1) {
          
            this.transform.localScale = Vector3.Lerp(InitialScale, FinalScale, progress);
            progress += Time.deltaTime * TimeScale;
            yield return null;
        }
        
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
            // FadeCanvas.GetComponent<Animation>().Play();

            StartCoroutine(wait());

        }
    }

    IEnumerator wait() {

        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("FinalScene");
    }


}
