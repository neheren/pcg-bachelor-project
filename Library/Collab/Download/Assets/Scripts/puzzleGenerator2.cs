using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleGenerator2 : MonoBehaviour {

	public List<string> objectsToSpawn;

	class PuzzleElement {
		public bool key;
		public string NAME;
		public List<string> generate;

		public PuzzleElement (string _NAME, List<string> _generate, bool _key) {
			NAME = _NAME;
			generate = _generate;
			key = _key;
		}

		public GameObject instantiate () {
			return (GameObject)Resources.Load (NAME);
		}
		
	}

	List<PuzzleElement> elements;

	public void Start () {
		const string PRESSURE_PLATE = "PRESSURE_PLATE";
		const string CUBE = "CUBE";
		const string LOCK = "LOCK";
		const string CUBE_DISPENSOR = "CUBE_DISPENCER";
		const string PLAYER_PRESSURE_PLATE = "PLAYER_PRESSURE_PLATE";
		const string BUTTON = "BUTTON";
		const string TOTEM = "Totem";

		elements = new List<PuzzleElement> () {
			//new PuzzleElement (LOCK, new List<string> (){ TOTEM }, false),
			new PuzzleElement (LOCK, new List<string> (){ PRESSURE_PLATE, PLAYER_PRESSURE_PLATE, BUTTON, TOTEM }, false),

			new PuzzleElement (PRESSURE_PLATE, new List<string> (){ CUBE, CUBE_DISPENSOR }, false),
			new PuzzleElement (CUBE_DISPENSOR, new List<string> (){ PLAYER_PRESSURE_PLATE, PRESSURE_PLATE, BUTTON, TOTEM }, false),

			new PuzzleElement (CUBE, new List<string> (0), true),
			new PuzzleElement (BUTTON, new List<string> (0), true),
			new PuzzleElement (PLAYER_PRESSURE_PLATE, new List<string> (0), true),
			new PuzzleElement (TOTEM, new List<string> (0), true)
		};
	}

	int chainSizeSkew = 3;

	public void generateSubWithSkew (int amount, string parent) {
		generateSubElements (amount - chainSizeSkew, parent);
	}

	private void generateSubElements (int amount, string parent) {
		PuzzleElement parentObject = findElement (elements, parent);
		if (parentObject == null)
			Debug.LogError ("The element " + parent + " was not found");
		if (amount >= 0 || parentObject.key == false) { // We must end on a key. Therefore we will continue until we find it. 
			objectsToSpawn.Add (parentObject.NAME);
			if (parentObject.generate.Count > 0) {
				// Run the function recursively again, if we are out of elements, try to find a key element so we can end the recursion.
				generateSubElements (amount - 1, amount < 0 ? findKeyElement (parentObject.generate) : findNonKeyElement (parentObject.generate)); 
			} else {
				if (!parentObject.key) {
					Debug.LogError (parent + " which is a non-key " + parentObject.key + "element has no sub elements");
				} else {
					Debug.LogError ("The recursion ended to early. Still missing " + amount + " iterations");
				}
			}
		} else if (parentObject.key == true) {
			objectsToSpawn.Add (parentObject.NAME);
//			print ("Recursion ended");
		} else {
			Debug.LogError ("This state should never be possible");
		}
	}


	PuzzleElement findElement (List<PuzzleElement> elementList, string NAME) {
		return elementList.Find ((o) => { // go through all keys and return the correct the ones we are looking for
			return (o.NAME == NAME); // we return that into the list. 
		});
	}


	string findKeyElement (List<string> converters) {
		return findStateElement (converters, true);
	}

	string findNonKeyElement (List<string> converters) {
		return findStateElement (converters, false);
	}

	string findStateElement (List<string> converters, bool keyState) {
		List<string> keys = converters.FindAll ((o) => {
			return findElement (elements, o).key == keyState;
		});
		if (keys.Count == 0) {
			return "";
		} else {
			return keys [Random.Range (0, keys.Count)];
		}
	}


}
