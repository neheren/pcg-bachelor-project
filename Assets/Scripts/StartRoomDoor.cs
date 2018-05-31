using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoomDoor : MonoBehaviour {


    private IEnumerator Start() {
        yield return new WaitForSeconds(1);
        GetComponent<TriggerRoomGeneration>().locked = false;

    }

  

}
