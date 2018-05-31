using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPressureLogic : MonoBehaviour {

    public WallDown WallDown;
	
	void Start () {

        WallDown = GameObject.FindWithTag("HiddenRoom").GetComponent<WallDown>();

	}


    private void OnCollisionEnter(Collision col) {

        if (col.transform.tag == "FinalPuzzleCube") {

            WallDown.ReadyToFall = true;
            WallDown.RunOnce = true;
        }
       

    }


     void OnCollisionExit(Collision col) {

        if (col.transform.tag =="FinalPuzzleCube") {

            WallDown.ReadyToFall = false;
            

        }

    }
}
