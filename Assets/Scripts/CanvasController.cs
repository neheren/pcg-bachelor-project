using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

	public GameObject RedText;
	public GameObject YellowText;
    public GameObject BlackText;


	void Start () {

   
		RedText = GameObject.Find ("RedDoor");
		YellowText = GameObject.Find ("YellowDoor");
        BlackText = GameObject.Find("BlackDoor");
        BlackText.SetActive(false);
		YellowText.SetActive (false);
		RedText.SetActive (false);

        
	}


	public IEnumerator RedDoorText () {

        YellowText.SetActive(false);
        BlackText.SetActive(false);
        RedText.gameObject.SetActive(true);
        RedText.GetComponent<Animation> ().Play ();
		yield return new WaitForSeconds (5);
		RedText.gameObject.SetActive (false);


	}

	public IEnumerator YellowDoorText () {

        RedText.SetActive(false);
        BlackText.SetActive(false);
        YellowText.gameObject.SetActive (true);
		YellowText.GetComponent<Animation> ().Play ();
		yield return new WaitForSeconds (5);
		YellowText.gameObject.SetActive (false);
       

	}

    public IEnumerator BlackDoorText() {

        YellowText.SetActive(false);
        RedText.SetActive(false);
        BlackText.gameObject.SetActive(true);
        BlackText.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(5);
        BlackText.gameObject.SetActive(false);


    }


}




