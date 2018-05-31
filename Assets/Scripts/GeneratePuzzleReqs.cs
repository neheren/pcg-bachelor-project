using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GeneratePuzzleReqs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ReadString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static void ReadString () {
		string path = "Assets/Resources/test.txt";
		StreamReader reader = new StreamReader (path); 
		Debug.Log (reader.ReadToEnd ());
		reader.Close ();
	}

}
