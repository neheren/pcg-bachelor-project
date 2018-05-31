using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {


	//float throwForce = 600;

	public bool canHold = true;
	public GameObject item;
	public GameObject tempParent;
	public Transform guide;
	public bool isHolding = true;
	private float distance;
	// Use this for initialization
	void Start () {
		item.GetComponent<Rigidbody> ().useGravity = false;
//		item.GetComponent<Rigidbody> ().isKinematic = false;

		item = this.gameObject;
		tempParent = GameObject.FindWithTag ("Guide");
		guide = GameObject.FindWithTag ("Guide").transform;

	}

	// Update is called once per frame
	void Update () {

		distance = Vector3.Distance (item.transform.position, guide.transform.position);

		if (isHolding == true) {
			item.GetComponent<Rigidbody> ().useGravity = false;
			item.GetComponent<Rigidbody> ().detectCollisions = true;
			item.GetComponent<Rigidbody> ().isKinematic = false;
			item.GetComponent<Rigidbody> ().freezeRotation = false;
			item.transform.parent = tempParent.transform;
			item.transform.position = guide.transform.position;
			/*if (Input.GetMouseButtonDown(1)) {
                Debug.Log("Trying to throw");
                item.GetComponent<Rigidbody>().AddForce(guide.transform.forward * throwForce);
                isHolding = false;
            }*/
		} else {
			item.GetComponent<Rigidbody> ().useGravity = true;
			item.GetComponent<Rigidbody> ().isKinematic = false;
			item.GetComponent<Rigidbody> ().freezeRotation = false;
			item.transform.parent = null;
		}
	}

	void OnMouseDown () {
		if (distance <= 1.5f) {
			isHolding = true;
			//guide.transform.position = item.transform.position;
		}
	}

	void OnMouseUp () {
		isHolding = false;
	}

	private void OnCollisionEnter (Collision collision) {

		if (collision.gameObject.tag == "RoomParts") {
			isHolding = false;
		}
       
	}

}