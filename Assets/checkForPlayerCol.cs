using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class checkForPlayerCol : MonoBehaviour {

    public GrowCube GrowCube;    

    void Start () {

        GrowCube = transform.GetChild(0).GetComponent<GrowCube>();

	}


     void OnTriggerEnter(Collider other) {

        if (other.transform.gameObject.tag == "Player"){

            gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine(GrowCube.GrowBigger());

        }

    }


}
