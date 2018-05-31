using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStartMusic : MonoBehaviour {


    public GameObject MapController;
    public bool musicIsPlaying = false;

    private void Start() {

        MapController = GameObject.Find("MapController");

    }

    private void OnTriggerEnter(Collider other) {

        if (other.transform.gameObject.tag == "Player" && !musicIsPlaying) {

            
            MapController.GetComponent<AudioSource>().Play();
            musicIsPlaying = true;           
        }
    }


}
