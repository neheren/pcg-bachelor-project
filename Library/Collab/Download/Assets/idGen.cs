using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idGen : MonoBehaviour {
	public int uniqueId;
	// Use this for initialization
	void Start () {
		uniqueId = Random.Range (0, 1000000);
		DontDestroyOnLoad (this);
	}

}
