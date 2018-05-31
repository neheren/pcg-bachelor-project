using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForDestroy : MonoBehaviour {

	CompCubeHit cube1;
	CompCubeHit cube2;
	CompCubeHit cube3;
	CompCubeHit cube4;
	public bool ReadyForFinalHit = false;
	public GameObject DestroyEffect2;
	public playerRaycasting playerRaycasting;
    public GunReach GunReach;



	private void Start () {
        GunReach = GameObject.FindWithTag("CompObject").GetComponent<GunReach>();
        DestroyEffect2 = (GameObject)Resources.Load ("DestroyEffect2");
		playerRaycasting = GameObject.FindWithTag ("Player").GetComponentInChildren<playerRaycasting> ();

		if (this.gameObject.name == "CompObject1(Clone)") {
			cube1 = transform.GetChild (1).GetComponent<CompCubeHit> ();
		}

		if (this.gameObject.name == "CompObject2(Clone)") {
			cube1 = transform.GetChild (1).GetComponent<CompCubeHit> ();
			cube2 = transform.GetChild (2).GetComponent<CompCubeHit> ();
			cube3 = transform.GetChild (3).GetComponent<CompCubeHit> ();
		}

		if (this.gameObject.name == "CompObject3(Clone)") {
			cube1 = transform.GetChild (1).GetComponent<CompCubeHit> ();
			cube2 = transform.GetChild (2).GetComponent<CompCubeHit> ();
			cube3 = transform.GetChild (3).GetComponent<CompCubeHit> ();
			cube4 = transform.GetChild (4).GetComponent<CompCubeHit> ();
		}

        if (this.gameObject.name == "CompObject4(Clone)") {
            cube1 = transform.GetChild(1).GetComponent<CompCubeHit>();
            cube2 = transform.GetChild(2).GetComponent<CompCubeHit>();
            cube3 = transform.GetChild(3).GetComponent<CompCubeHit>();
            cube4 = transform.GetChild(4).GetComponent<CompCubeHit>();  


        }


	}

	public void checkfordestroy () {

		if (this.gameObject.name == "CompObject1(Clone)" && cube1.readyToKill == true) {
			print ("Ødelæg mig");
			GameObject EffectIns = (GameObject)Instantiate (Resources.Load ("DestroyEffect2"), this.gameObject.transform.position, new Quaternion ());
			unlockDoor ();
            GunReach.GunIsReady = false;
            GunReach.GunObject.SetActive(false);
            Destroy (EffectIns, 2f);
			Destroy (this.gameObject);
		}


		if (this.gameObject.name == "CompObject2(Clone)" && cube1.readyToKill == cube2.readyToKill && cube2.readyToKill == cube3.readyToKill) {

			print ("Ødelæg mig");
			GameObject EffectIns = (GameObject)Instantiate (Resources.Load ("DestroyEffect2"), this.gameObject.transform.position, new Quaternion ());
			unlockDoor ();
            GunReach.GunIsReady = false;
            GunReach.GunObject.SetActive(false);
            Destroy (EffectIns, 2f);
			Destroy (this.gameObject);

		}


		if (this.gameObject.name == "CompObject3(Clone)" && cube1.readyToKill == cube2.readyToKill && cube2.readyToKill == cube3.readyToKill && cube3.readyToKill == cube4.readyToKill) {

			print ("Ødelæg mig");
			GameObject EffectIns = (GameObject)Instantiate (Resources.Load ("DestroyEffect2"), this.gameObject.transform.position, new Quaternion ());
			unlockDoor ();
            GunReach.GunObject.SetActive(false);
            Destroy (EffectIns, 2f);
			Destroy (this.gameObject);
		}

        if (this.gameObject.name == "CompObject4(Clone)" && cube1.readyToKill == cube2.readyToKill && cube2.readyToKill == cube3.readyToKill && cube3.readyToKill == cube4.readyToKill) {

            print("Ødelæg mig");
            GameObject EffectIns = (GameObject)Instantiate(Resources.Load("DestroyEffect2"), this.gameObject.transform.position, new Quaternion());
            unlockDoor();
            GunReach.GunObject.SetActive(false);
            Destroy(EffectIns, 2f);
            Destroy(this.gameObject);
        }
        else {

            return;
        }
        
	}

	void unlockDoor () {
		GetComponent<connectToDoor> ().unlockDoor ();
	}
}
