using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDown : MonoBehaviour {

    public bool ReadyToFall = false;
    public bool RunOnce = false;
	
	void Start () {

        transform.GetComponentInChildren<Rigidbody>().useGravity = false;

	}


    private void Update() {

        if (ReadyToFall && RunOnce) {

            Rigidbody[] rigidBodies;
            BoxCollider[] boxColliders;

            boxColliders = transform.GetComponentsInChildren<BoxCollider>();
            rigidBodies = transform.GetComponentsInChildren<Rigidbody>();
            RunOnce = false;

            foreach (var col in boxColliders) {

                col.enabled = false;

            }

            foreach (var item in rigidBodies) {

                
                StartCoroutine(Delay(item));

            }

        }


    }


    IEnumerator Delay(Rigidbody item) {

        yield return new WaitForSeconds(Random.Range(0.2f,1f));
        item.useGravity = true;
    }

}
