using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour {
	public bool allowShooting;
	public Animation ani;
	public AudioSource audio;
	public Transform flashPos;
	public GameObject flash;
	// Use this for initialization
	void Start () {
		ani = GetComponent<Animation> ();
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable () {
		ani = GetComponent<Animation> ();
		ani.Stop ();
		ani.Play ("gun_bring_up");
		ani.PlayQueued ("gun_idle");
	}

	public IEnumerator animateAndDisable () {
		ani.Stop ();
		ani.Play ("gun_bring_down");
		yield return new WaitWhile (() => {
			return ani.isPlaying;
		});
		transform.parent.gameObject.SetActive (false);
	}

	public bool shot () {
		if (allowShooting) {
			Instantiate (flash, flashPos.position, Quaternion.Euler (new Vector3 ()));
			ani.Stop ();
			ani.Play ("gun_shoot");
			ani.PlayQueued ("gun_idle");
			audio.Play ();
			return true;
		}
		return false;

	}
}
