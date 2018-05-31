using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

public class StoryController : MonoBehaviour {
	public string[] stringArray;
	public int currentString = -1;
	public int newestRoomStoryUnread;

	private void Start () {
		ReadString ();

	}

	void ReadString () {
		TextAsset toLoad = (TextAsset)Resources.Load ("Story");
		stringArray = toLoad.text.Split ('%');
	}

	public string getStory () {
		currentString++;
		return stringArray [currentString];
	}
}

