using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

public class DestroyCube : MonoBehaviour {

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController Controller;
    //public MoveObject MoveObject;
    public GameObject DestroyEffect;
    public TriggerRoomGeneration TriggerRoomGeneration;

    private void Start() {

        
        DestroyEffect = (GameObject)Resources.Load("DestroyEffect");
        TriggerRoomGeneration = transform.GetChild(0).GetComponent<TriggerRoomGeneration>();
        Controller = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        Controller.tag = "Player";



    }
    void OnTriggerEnter(Collider other) {

       
        if (other.gameObject.tag == "cube" && TriggerRoomGeneration.openOnce==false)     {
            print("destroyCube");
            DestroyEffect.GetComponent<AudioSource>().Play();
            GameObject effectIns = (GameObject)Instantiate(Resources.Load("DestroyEffect"), Controller.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform.position, new Quaternion() );
            Destroy(effectIns, 2f);
            Destroy(Controller.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
        }
    }

}
