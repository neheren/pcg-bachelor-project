using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo {
	public Vector2 generationSize;
	public Vector2 subRoomSize;
	public int subRoomAmount;
	public int height;
	public bool quadric;
	char[] doorDirs;
	public Color roomColor;
	public GameObject[] objects;

	public RoomInfo (Vector2 _generationSize, Vector2 _subRoomSize, int _subRoomAmount, int _height, bool _quadric, Color _roomColor) {
		
	}

	public RoomInfo () {
		
	}
}

public class generateRoom : MonoBehaviour {
	public GameObject room;
	public GameObject roof;
	public GameObject floor;
	public GameObject wall;
	public GameObject objective;
	public GameObject door;


	Vector2[] exits;
	PlayerBehavouir playerBehavouir;

	public void StartGeneration (char direction) {		
		mapController = GameObject.FindWithTag ("GameController").GetComponent<MapController> ();
		mapControllerBackUpRef = GameObject.Find ("MapController").GetComponent<MapController> ();
		playerBehavouir = GameObject.FindWithTag ("GameController").GetComponent<PlayerBehavouir> ();
		StartCoroutine (generateMapAsync (direction, new RoomInfo ()));
	}

	MapController mapController;
	public MapController mapControllerBackUpRef;

	IEnumerator generateMapAsync (char direction, RoomInfo room) {
		List<GameObject> emptyParentList = new List<GameObject> ();
		List<GameObject> floorMeshCombine = new List<GameObject> ();
		List<GameObject> wallMeshCombine = new List<GameObject> ();
		List<GameObject> roofMeshCombine = new List<GameObject> ();
		exits = new Vector2[3] { new Vector2 (0, 0), new Vector2 (0, 0), new Vector2 (0, 0) };

		int roomSizeStep = 4;
		int currentRoomSize = (mapController.finalRoom) ? 40 : Mathf.Clamp (30 + (playerBehavouir.PuzzleCompleted + mapController.roomAmount / 2) * roomSizeStep, 0, 40);

		Vector2 roomSize = new Vector2 (currentRoomSize, currentRoomSize);

		List<GameObject> centerSpawnPoints = new List<GameObject> ();
		List<GameObject> wallSpawnPoints = new List<GameObject> ();

		int spawnPointThresh = mapController.finalRoom ? 1 : 3;
		char[,] map = generateMap (roomSize);
		while (multipleBlobs (map) || !enoughSpawnPoints (map, spawnPointThresh)) {
			print ("re-generating map");
			yield return new WaitForEndOfFrame ();
			map = generateMap (roomSize);
		}
		addDoorPositions (map, direction);

		yield return new WaitForEndOfFrame ();
		bool[] usedDoors = new bool[4]{ false, false, false, false };

		for (int x = 0; x < map.GetLength (0); x++) {
			string line = "";
			yield return new WaitForFixedUpdate ();
			for (int y = 0; y < map.GetLength (1); y++) {
				Transform room_parts_child = transform.GetChild (0).transform;

				if (map [x, y] == 'f' || map [x, y] == 'r')
					floorMeshCombine.Add (Instantiate (floor, transform.position + new Vector3 (x, 0, y), Quaternion.Euler (0, 0, 0), room_parts_child));
				if (map [x, y] == 'w') {
					GameObject spawnWall = Instantiate (wall, transform.position + new Vector3 (x, 0, y), Quaternion.Euler (0, 0, 0), room_parts_child);
					wallMeshCombine.Add (spawnWall.transform.GetChild (0).gameObject);
				}
				if (map [x, y] == 'o') { // o is center spawnplacements
					Instantiate (floor, transform.position + new Vector3 (x, 0, y), Quaternion.Euler (0, 0, 0), room_parts_child);
					centerSpawnPoints.Add (Instantiate (objective, transform.position + new Vector3 (x, 0, y), Quaternion.Euler (0, 0, 0), room_parts_child));
				}

				if (map [x, y] == 'y') { // y is wall spawnplacements
					Instantiate (floor, transform.position + new Vector3 (x, 0, y), Quaternion.Euler (0, 0, 0), room_parts_child);
					wallSpawnPoints.Add (Instantiate (objective, transform.position + new Vector3 (x, 0, y), Quaternion.Euler (0, 0, 0), room_parts_child));
				}

				if (map [x, y] == 'x') { // y is wall spawnplacements
					Instantiate (Resources.Load (mapController.finalRoomName), transform.position + new Vector3 (x, 0, y), Quaternion.Euler (0, 0, 0), room_parts_child);
					wallSpawnPoints.Add (Instantiate (objective, transform.position + new Vector3 (x, 0.5f, y), Quaternion.Euler (0, 0, 0), room_parts_child));
				}


				if (map [x, y] != ' ') {
					if (true || Random.Range (0f, 1f) < 0.95f) {
						roofMeshCombine.Add (Instantiate (roof, transform.position + new Vector3 (x, 4f, y), Quaternion.Euler (0, 0, 0), room_parts_child));
					}
				}

				for (int i = 0; i < 4; i++) {
					if (map [x, y] == doors [i] && doors [i] != oppositeDirection (direction)) {
						GameObject currentDoor = Instantiate (door, transform.position + new Vector3 (x, 0, y), Quaternion.Euler (0, 0, 0), this.transform);
						currentDoor.GetComponentInChildren<TriggerRoomGeneration> ().direction = doors [i];
						if (usedDoors [i])
							currentDoor.GetComponentInChildren<TriggerRoomGeneration> ().used = true;
						currentDoor.GetComponentInChildren<TriggerRoomGeneration> ().position = doorPostions [i] + new Vector2 (transform.position.x, transform.position.z);
						usedDoors [i] = true;
					}
				}
				//line += map [x, y];
			}
			//print (line);
		}
		GetComponent<ManageRoomComponents> ().centerSpawnPlacements = centerSpawnPoints;
		GetComponent<ManageRoomComponents> ().wallSpawnPlacements = wallSpawnPoints;
		roofMeshCombine.InsertRange (0, wallMeshCombine);
		roofMeshCombine.InsertRange (0, floorMeshCombine);
//		print ("Amount of meshes: " + roofMeshCombine.Count);
		IEnumerator bake = GetComponent<slytMeshCombine> ().run (roofMeshCombine, name); 
		try {
			mapController.bakeQueue.Enqueue (bake);
		} catch {
			Debug.LogError ("GAMECONTROLLER NOT FOUND FOR BAKING. Using backup ref");
			try {
				mapControllerBackUpRef.bakeQueue.Enqueue (bake);
				print ("found");
			} catch {
				Debug.LogError ("STILL NOT FOUND");
				//SceneManager.LoadScene ("error"); 
			}
		}
	}



	char[,] generateMap (Vector2 size) {
		char[,] map = new char[(int)size.x, (int)size.y];
		for (int x = 0; x < map.GetLength (0); x++) {
			for (int y = 0; y < map.GetLength (1); y++) {
				map [x, y] = ' ';
			}
		}

		float d = 4f / 1000f; // this is our 'density' which determains the complexity of a room, no matter the size.
		float A = map.GetLength (0) * map.GetLength (1);
		int n = (mapController.finalRoom) ? 1 : Mathf.RoundToInt ((float)A * d);

		for (int i = 0; i < n; i++) {
			map = generateSubRoom (map, 
				new Vector2Int (randomLength (5, map.GetLength (0)), randomLength (5, map.GetLength (1))), 
				new Vector2Int (randomLength (0, map.GetLength (0)), randomLength (0, map.GetLength (1)))
			);
		}
		return map;
	}


	int randomLength (int min, int max) {
		return (mapController.finalRoom) ? max / 2 : Random.Range (min, max / 2);
	}

	char[,] generateSubRoom (char[,] map, Vector2Int roomSize, Vector2Int start) {
		Vector2Int realRoomSize = roomSize;
		roomSize += start;
		for (int x = start.x; x < roomSize.x; x++) {
			for (int y = start.y; y < roomSize.y; y++) {
				if (map [x, y] != 'o') {
					if (map [x, y] == 'w')
						map [x, y] = 'f';
					
					if (x == start.x || x == roomSize.x - 1 || y == start.y || y == roomSize.y - 1) {
						if (map [x, y] != 'r')
							map [x, y] = 'w';
					} else {
						map [x, y] = 'r';
					}
					
					if (x == start.x + realRoomSize.x / 2 && y == start.y + realRoomSize.y / 2) {
						map [x, y] = 'o';
						if (mapController.finalRoom) {
							map [x, y] = 'x';
						}
					}
					if ((realRoomSize.x > 5 && realRoomSize.y > 5)) {
						if ((x == roomSize.x - 2 || x == start.x + 1) && y == start.y + realRoomSize.y / 2) {
							map [x, y] = 'y';
						}
					}
				}

			}
		}
		return map;
	}

	char oppositeDirection (char direction) {
		switch (direction) {
			case 'S':
				return 'N';
			case 'N':
				return 'S';
			case 'E':
				return 'W';
			case 'W':
				return 'E';
			default:
				Debug.LogError ("Can't find opposite direction. Input direction must be either: N, S, W, E");
				return ' ';
		}

	}

	char[] doors = new char[4]{ 'S', 'N', 'E', 'W' };
	Vector2[] doorPostions = new Vector2[4];


	Vector2Int addDoorPositions (char[,] map, char direction) {
		int highestX = 0, lowestX = 1000, highestY = 0, lowestY = 1000;
		int[] borders = new int[]{ highestX, lowestX, highestY, lowestY };

		for (int x = 0; x < map.GetLength (0); x++) {
			for (int y = 0; y < map.GetLength (1); y++) {
				if (map [x, y] == 'w') {
					if (x > borders [0])
						borders [0] = x;
					if (x < borders [1])
						borders [1] = x;
					if (y > borders [2])
						borders [2] = y;
					if (y < borders [3])
						borders [3] = y;
				}
			}
		}


		int[] hit = new int[4] { 0, 0, 0, 0 };

		//DOOR PLACEMENT
		for (int x = 0; x < map.GetLength (0); x++) {
			for (int y = 0; y < map.GetLength (1); y++) {
				if (map [x, y] == 'w') {
					int[] compare = new int[]{ x, x, y, y };

					for (int i = 0; i < 4; i++) {
						if (compare [i] == borders [i]) {
							hit [i]++;
							if (hit [i] > 2 && hit [i] < 5) {
								map [x, y] = doors [i];
								Debug.DrawLine (Vector3.zero, new Vector3 (x, 0, y), new Color (i / 4f, i / 4f, i / 4f), 1000);
								doorPostions [i] = new Vector2 (x, y);
							}
						}
					}
				}
			}
		}
			
		if (direction == 'N')
			transform.position -= new Vector3 (doorPostions [0].x, 0, doorPostions [0].y);
		if (direction == 'S')
			transform.position -= new Vector3 (doorPostions [1].x, 0, doorPostions [1].y);
		if (direction == 'W')
			transform.position -= new Vector3 (doorPostions [2].x, 0, doorPostions [2].y);
		if (direction == 'E')
			transform.position -= new Vector3 (doorPostions [3].x, 0, doorPostions [3].y);

		for (int i = 0; i < 3; i++) {
			Debug.DrawLine (Vector3.zero, new Vector3 (exits [i].x, 3, exits [i].y), Color.blue, 10000);
		}


		return new Vector2Int (0, 0);
	}


	bool enoughSpawnPoints (char[,] map, int amount) {
		int spawnPoints = 0;
		for (int x = 0; x < map.GetLength (0); x++) {
			for (int y = 0; y < map.GetLength (1); y++) {
				if (map [x, y] == 'o' || map [x, y] == 'x') {
					spawnPoints++;
				}
			}
		}
		//	print ("(should be true)Enough spawnPlacements " + (spawnPoints >= amount));
		//	print ("generated " + spawnPoints + " spawnpoints should be at least " + amount);
		return (spawnPoints >= amount);
	}

	bool multipleBlobs (char[,] map) {
		bool[,] binaryMap = new bool[map.GetLength (0), map.GetLength (1)];

		for (int x = 0; x < map.GetLength (0); x++) {
			for (int y = 0; y < map.GetLength (1); y++) {
				binaryMap [x, y] = (map [x, y] == 'r') ? true : false;
			}
		}

		Blob[] blobs = grassFire (binaryMap);
//		print ("(should be false)multiple blobs: " + (blobs.Length > 2));
		return (blobs.Length > 2);
	}


	public static Blob[] grassFire (bool[,] inputPicture) {
		float[,] blobsGradient = new float[inputPicture.GetLength (0), inputPicture.GetLength (1)];
		float[,] blobsEdges = new float[inputPicture.GetLength (0), inputPicture.GetLength (1)];
		int blobTag = 1;
		Queue<int> qX = new Queue<int> ();
		Queue<int> qY = new Queue<int> ();
		int[] kernelX = { 0, 1, 0, -1 };
		int[] kernelY = { -1, 0, 1, 0 };
		for (int y = 2; y < inputPicture.GetLength (1) - 2; y++) {
			for (int x = 2; x < inputPicture.GetLength (0) - 2; x++) {
				if (inputPicture [x, y] == true) {
					qX.Enqueue (x);
					qY.Enqueue (y);
					inputPicture [x, y] = false;
					blobsGradient [x, y] = blobTag;
					while (qX.Count != 0 && qY.Count != 0) {
						for (int i = 0; i < 4; i++) {
							if (inputPicture [qX.Peek () + kernelX [i], qY.Peek () + kernelY [i]]) {
								qX.Enqueue (qX.Peek () + kernelX [i]);
								qY.Enqueue (qY.Peek () + kernelY [i]);
								inputPicture [qX.Peek () + kernelX [i], qY.Peek () + kernelY [i]] = false;
								blobsGradient [qX.Peek () + kernelX [i], qY.Peek () + kernelY [i]] = (float)blobTag;
							} else if (blobsGradient [qX.Peek () + kernelX [i], qY.Peek () + kernelY [i]] == 0) {
								blobsEdges [qX.Peek (), qY.Peek ()] = (float)blobTag;
							}
						}
						qX.Dequeue ();
						qY.Dequeue ();
					}
					blobTag += 1;
				}
			}
		}

		Blob[] blobs = BlobFeatures (blobsGradient, blobsEdges, blobTag);

		return blobs;
	}


	private static Blob[] BlobFeatures (float[,] blobsGradient, float[,] blobsEdges, int blobTag) {
		//INITIALIZING:
		Blob[] blobs = new Blob[blobTag];
		for (int i = 0; i < blobTag; i++) {
			blobs [i] = new Blob ();
		}
		//FINDING CENTER OF MASS & AREA:
		for (int y = 2; y < blobsGradient.GetLength (1) - 2; y++) {
			for (int x = 2; x < blobsGradient.GetLength (0) - 2; x++) {
				if (blobsGradient [x, y] != 0f) {
					blobs [(int)blobsGradient [x, y]].CenterOfMass.y += y;
					blobs [(int)blobsGradient [x, y]].CenterOfMass.x += x;
					blobs [(int)blobsGradient [x, y]].area++;
				}
				if (blobsEdges [x, y] != 0f) {
					blobs [(int)blobsEdges [x, y]].edgeArea++;
				}
			}
		}
		for (int i = 0; i < blobs.Length; i++) {
			blobs [i].CenterOfMass.x = blobs [i].CenterOfMass.x / blobs [i].area;
			blobs [i].CenterOfMass.y = blobs [i].CenterOfMass.y / blobs [i].area;
		}

		return blobs;
	}


	public class Blob {
		public int tagNumber;
		public int area;
		public Vector2 CenterOfMass = new Vector2 (0, 0);
		public string type;
		public int corners;
		public int edgeArea;
	}




}
