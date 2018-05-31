using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavouir : MonoBehaviour {
	MapController mapController;
	public int puzzleChainSize = 1;
	public float storySize = 1;
	public int compDifficulty = 0;
	List<int> movingStoryCompletion = new List<int> (){ 0, 0, 1, 0 };
	List<int> movingPuzzleCompletion = new List<int> (){ 1, 0, 1, 0 };
	List<int> movingCompCompletion = new List<int> (){ 1, 0, 1, 0 };
	private int puzzlesCompleted = 0;
	private int storiesCompleted = 0;
	private int compCompleted = 0;
	int completionThreshold = 10;
	const string A = "A";
	const string B = "B";
	public string prototype;
	bool enablePlayerBehavouir;
	int storyAmount = 12;
	int compAmount = 7;
	int puzzleAmount = 7;

	public int StoriesCompleted {
		get {
			return storiesCompleted;
		}
		set {
			if (value > storyAmount) {
				mapController.finalRoom = true;
				mapController.finalRoomName = "FinalStory";
			}

			movingStoryCompletion.Add (1);
			if (value % 2 == 0) {
				movingPuzzleCompletion.Add (0);
				movingCompCompletion.Add (0);
			}
			storySize = storySize + ((storiesCompleted % 3 == 2) ? 1 : 0);
			storiesCompleted = value;
		}
	}

	public int PuzzleCompleted {
		get {
			return puzzlesCompleted;
		}
		set {
			if (value > puzzleAmount) {
				mapController.finalRoom = true;
				mapController.finalRoomName = "FinalPuzzleRoom";
			}
			movingPuzzleCompletion.Add (1); 
			movingStoryCompletion.Add (0);
			movingCompCompletion.Add (0);
			puzzleChainSize = puzzleChainSize + ((puzzlesCompleted % 2 == 1) ? 1 : 0);
			puzzlesCompleted = value;
		}
	}

	public int CompCompleted {
		get {
			return compCompleted;
		}

		set {
			if (value > compAmount) {
				mapController.finalRoom = true;
				mapController.finalRoomName = "FinalComp";
			}
			movingCompCompletion.Add (1);
			movingPuzzleCompletion.Add (0);
			movingStoryCompletion.Add (0);

			compDifficulty = compDifficulty + ((compCompleted % 2 == 1) ? 1 : 0);
			compCompleted = value;
		}
	}

	void Start () {
		enablePlayerBehavouir = false; // from server. use consts 
		mapController = GetComponent<MapController> ();
		DeterminePrototype prototypepicker = GetComponent<DeterminePrototype> ();
		StartCoroutine (prototypepicker.determinePrototype (r => {
			print ("Got prototype " + r);
			prototype = r;
			if (r == A) {
				enablePlayerBehavouir = true;
			} else if (r == B) {
				enablePlayerBehavouir = false;
			} else {
				Debug.LogError ("error determaining prototype");
			}
//			enablePlayerBehavouir = true; // OVERWRITE
			//StartCoroutine (prototypepicker.confirmPrototype (r));
		}));
	}

	float calculateMovingAvg (int maxElements, int totalCompletion, List<int> movingAvg) {
		// float calc = (float)maxElements * ((float)elementsCompleted / (float)totalCompletion);
		float calc = 0;
		for (int i = movingAvg.Count - 4; i < movingAvg.Count; i++) {
			calc += movingAvg [i];
		}
		calc += 1; // adding one to increase the chances. 
		calc /= 5f;
		return Mathf.Min (calc, (float)maxElements);
	}

	public int calculateStoryAmount () {
		return (enablePlayerBehavouir) ? Mathf.RoundToInt (calculateMovingAvg (2, StoriesCompleted, movingStoryCompletion) + Random.Range (0f, 1f)) : 1;
	}


	public int calculateCompDifficulty () { // still not disabled if users are not using it!!
		int maxDifficulty = 6;
		return (enablePlayerBehavouir) ? Mathf.RoundToInt ((calculateMovingAvg (maxDifficulty, CompCompleted, movingCompCompletion) * compDifficulty) + Random.Range (0f, 0.5f)) : 2;
	}


	private float calculatePuzzleChainMovingAvg () {
		return calculateMovingAvg (5, mapController.roomAmount, movingPuzzleCompletion);
	}

	public int chainSize () {
		return (enablePlayerBehavouir) ? Mathf.RoundToInt ((float)puzzleChainSize * calculatePuzzleChainMovingAvg () + Random.Range (0f, 0.5f)) : puzzleChainSize;
	}

	void OnGUI () {
		#if(UNITY_EDITOR)
		if (enablePlayerBehavouir) {
			GUI.Box (new Rect (20, 20, 350, 25), "<color=\"red\">Puzzles completed: </color>" + PuzzleCompleted + " chain length " + chainSize ());
			GUI.Box (new Rect (20, 50, 350, 25), "<color=\"red\">Puzzle info: Moving avg: </color>" + calculatePuzzleChainMovingAvg () + " chainSize " + puzzleChainSize);
			GUI.Box (new Rect (20, 80, 350, 25), "<color=\"yellow\">Stories completed: </color>" + StoriesCompleted + " spawning " + calculateStoryAmount () + " storie(s)");
			GUI.Box (new Rect (20, 110, 350, 25), "Comp elem completed: " + CompCompleted + " diffuculty: " + calculateCompDifficulty ());
			GUI.Box (new Rect (20, 150, 350, 25), "Generated levels: " + mapController.roomAmount);
		} else {
			GUI.Box (new Rect (20, 20, 350, 25), "Player behavouir disabled.");
			GUI.Box (new Rect (20, 50, 250, 25), "Stories completed: " + StoriesCompleted + " spawning " + calculateStoryAmount () + " storie(s)");
			GUI.Box (new Rect (20, 80, 250, 25), "Puzzles completed: " + PuzzleCompleted + " chain length " + chainSize ());
			GUI.Box (new Rect (20, 110, 250, 25), "Comp elem diffuculty: " + compDifficulty);
		}
		#endif
	}
}
