using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleGenerator : MonoBehaviour {

	/// <summary>
	/// A key is what is used to 
	/// </summary>
	public struct Key {
		public string objectSpawnName;
		public string TYPE;
		public List<Converter> converters;

		public Key (string _type, string _spawnName) {
			TYPE = _type;
			objectSpawnName = _spawnName;
			converters = new List<Converter> ();
		}

		public Key (string _type, string _spawnName, List<Converter> _converters) {
			TYPE = _type;
			objectSpawnName = _spawnName;
			converters = _converters;
		}
	}

	public struct Converter {
		public string objectSpawnName;
		public string TYPE;
		public List<Key> keys;
		public List<Converter> converters;

		public Converter (string _type, string _spawnName, List<Key> _keys) {
			TYPE = _type;
			objectSpawnName = _spawnName;
			keys = _keys;
			converters = new List<Converter> (); // just an empty converter list
		}

		public Converter (string _type, string _spawnName, List<Key> _keys, List<Converter> _converters) {
			TYPE = _type;
			objectSpawnName = _spawnName;
			keys = _keys;
			converters = _converters;
		}
	}

	public struct Lock {
		public string objectSpawnName;
		public string TYPE;
		public List<Converter> converters;

		public Lock (string _type, string _spawnName, List<Converter> _converters) {
			TYPE = _type;
			objectSpawnName = _spawnName;
			converters = _converters;
		}
	}

	int keyAmount = 1;

	void Start () {
		List<string> objectsSpawnNames = new List<string> ();

		List<Key> keys = new List<Key> ();
		keys.Add (new Key ("CUBE", "cube"));
		keys.Add (new Key ("LASER", "laser_pointer"));
		keys.Add (new Key ("SAG", "sag"));

		List<Converter> converters = new List<Converter> ();
		converters.Add (new Converter ("PRESSURE_PLATE", "pressure_plate", findKey (keys, new string []{ "CUBE", "SAG" })));
		converters.Add (new Converter ("LASER_MIRROR", "pressure_plate", findKey (keys, new string []{ "LASER" })));

		List<Lock> locks = new List<Lock> ();
		locks.Add (new Lock ("PRESSURE_LOCK", "spawn_lock", findConverter (converters, new string[] {
			"PRESSURE_PLATE",
		})));




	}

	List<Key> findKey (List<Key> keys, string[] NAMES) {
		List<Key> foundKeys = keys.FindAll ((o) => { // go through all keys and return the correct the ones we are looking for
			bool found = false; // did we find the object
			foreach (var NAME in NAMES) { // lets search all input names for one of the 'o' objects
				found = (o.TYPE == NAME) ? true : found; // if we found one of the NAMES in one a key, 
			}
			return found; // we return that into the list. 
		});
		if (foundKeys.Count != NAMES.Length) {
			Debug.LogError ("Key not found, please check for typos in puzzleGenerator:Start");
		}
		return foundKeys;
	}

	List<Converter> findConverter (List<Converter> converters, string[] NAMES) {
		List<Converter> foundConverter = converters.FindAll ((o) => { // go through all keys and return the correct the ones we are looking for
			bool found = false; // did we find the object
			foreach (var NAME in NAMES) { // lets search all input names for one of the 'o' objects
				found = (o.TYPE == NAME) ? true : found; // if we found one of the NAMES in one a key, 
			}
			return found; // we return that into the list. 
		});
		if (foundConverter.Count != NAMES.Length) {
			Debug.LogError ("Converter not found, please check for typos or missing converters in puzzleGenerator:Start");
		}
		return foundConverter;
	}


	void generateReqs () {
		
	}

}
