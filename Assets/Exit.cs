using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Exit : MonoBehaviour {


    private void Start() {

        Cursor.visible = true;
            
}


    public void ExitGame() {

        Application.Quit();


    }

}
