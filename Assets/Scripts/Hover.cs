using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

    float t;
    float a;
    float x;
    float y;
    float z;

    void Start() {

        y = this.gameObject.transform.position.y;
        x = this.gameObject.transform.position.x;
        z = this.gameObject.transform.position.z;

        t = Random.Range(0f, 30f);
        a = Random.Range(0.1f, 0.2f);


    }


    void Update() {



        gameObject.transform.position = new Vector3(x, y + Mathf.Sin(t) * a, z);
        t += 0.02f;



    }
}
