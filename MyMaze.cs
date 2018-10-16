using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMaze : MonoBehaviour {

	public GameObject wall;
	public GameObject player;
	public static int MazeHeight;
	public static int MazeWidth;
	public GameObject cell;
	public GameObject coin;
	public GameObject key;
	public GameObject door;
	private List<int> coinPositions = new List<int>();
	private List<GameObject> mazePositions = new List<GameObject>();

	public static int maxCoins;



	private int[,] maze;
	private GameObject wallHolder;
	private List<GameObject> cells = new List<GameObject>();

	static System.Random _random = new System.Random();

	// Use this for initialization
	void Start () {
		Item.totalCoinsPickedUp = 0;
		if(MazeHeight == 0){ MazeHeight = 9;}		
		if(MazeWidth == 0) { MazeWidth = 9;}
		//run maze generator
		maze = GenerateMaze(MazeHeight, MazeWidth);
		//show maze
		wallHolder = new GameObject ();
		wallHolder.name = "Maze";

		for(int i = 0; i < MazeHeight; i++){
			for(int j = 0; j < MazeWidth; j++){
				// make wall if there's a 1 in array
				if(maze[i,j] == 1){
					Vector3 pos = new Vector3(i*4.0f, 2.5f, j*4.0f);
					GameObject newWall = Instantiate(wall) as GameObject;
					if( newWall != null){
						newWall.transform.parent = wallHolder.transform;
						newWall.transform.position = pos;
						newWall.name = "Wall["+ newWall.transform.position.x / 4 + "," + newWall.transform.position.z / 4 + "]";

						//populate into the maze positions List for checking placement of other objects (door, lights)
						mazePositions.Add(newWall);
					}
					} else {
						Vector3 cellPos = new Vector3(i*4.0f, 2.5f, j*4.0f);
						GameObject newCell = Instantiate(cell, cellPos, Quaternion.identity) as GameObject;
						newCell.name = "Cell["+ newCell.transform.position.x / 4 + "," + newCell.transform.position.z / 4 + "]";
						cells.Add(newCell);

						//populate into the maze positions List for checking placement of other objects (door, lights)
						mazePositions.Add(newCell);

					}
			}
		}

		//call method to place the camera/player
		PlacePlayer();
		PlaceCoins();
		PlaceKey();
		PlaceExit();
	}

	private void PlacePlayer(){
		//this places the player in the first cell created in the maze
		GameObject firstCell = cells[0];
		Instantiate(player, new Vector3( firstCell.transform.position.x, 0.5f, firstCell.transform.position.z), Quaternion.identity);
//		Instantiate(player, new Vector3(100f, 0,100f), Quaternion.identity);
	}


	private void PlaceCoins(){
		//place coins randomly throughout the maze
		//based on the number of cells created - divide by 10 for number of coins to place
		int totalCoinsPlaced = 0;
		Debug.Log("Cell Count: " + cells.Count);
		maxCoins = cells.Count / 5;
		int coinIndex;
		//take a random number from the total number of cells
		while(totalCoinsPlaced < maxCoins){
			//assign the coin initially to a random spot
			coinIndex = Random.Range(0, cells.Count);
			if(coinPositions.Contains(coinIndex)){
				break;
			} else {
				coinPositions.Add(coinIndex);
				totalCoinsPlaced++;
			}
		}
		for(int i = 0; i < coinPositions.Count; i++){
			GameObject coinCell = cells[coinPositions[i]];
			float xPos = coinCell.transform.position.x;
			float zPos = coinCell.transform.position.z;
			coin = Instantiate(coin, new Vector3(xPos, 1.5f, zPos), Quaternion.identity) as GameObject;
			coin.name = "Coin";
		}

	}

	private void PlaceKey(){
		int keySpot = Random.Range(0, cells.Count / 2);
		bool keySpotFound = false;
		while(!keySpotFound){
			if(coinPositions.Contains(keySpot)){
				keySpot = Random.Range(cells.Count / 2, cells.Count);
			} else {
				GameObject cellForKey = cells[keySpot];
				key = Instantiate(key, new Vector3(cellForKey.transform.position.x, 1.5f, cellForKey.transform.position.z), Quaternion.identity) as GameObject;
				key.name = "Key";
				keySpotFound = true; 
			}
		}
	}

	private void PlaceExit(){
		float height = MazeHeight-2;
		float width = MazeWidth-2;
		Instantiate(door, new Vector3(height*4, 2f, (width*4 + 2)), Quaternion.identity);
	}

	private int[,] GenerateMaze(int height, int width){
		int[,] maze = new int[height, width];
		//init all the walls to 1
		for(int i = 0; i < height; i++){
			for(int j = 0; j < width; j++){
				 maze[i,j] = 1;
			}
		}
		// generate new random seed
		System.Random rand = new System.Random();

		// find random start point
		int num = rand.Next(height);
		while(num % 2 == 0){
			num = rand.Next(height);
		}
		int numRow = rand.Next(width);
		while(numRow % 2 == 0){			
			numRow = rand.Next(width);
		}	

		// set start cell to path, value = 0
		maze[num, numRow] = 0;

		// create out maze using dfs
		MazeDigger(maze, num, numRow);

		// return the maze
		return maze;

	}

	private void MazeDigger(int[,] maze, int r, int c){
		// create digging directions
		// assign num to direction 1N 2S 3E 4W

		int[] directions = new int[] {1,2,3,4};
		Shuffle(directions);

		// Look in random direction
		for(int i = 0; i < directions.Length; i++)
		{
			switch(directions[i]){
				case 1: // north
					// check if 2 cells north is out of range
					if(r - 2 <= 0)
						continue;
					if(maze[r - 2, c] != 0){
						maze[r-2, c] = 0;
						maze[r-1, c] = 0;
						MazeDigger(maze, r - 2, c);
					}
					break;
			case 2: // south
					// check if 2 cells north is out of range
					if(r + 2 >= MazeHeight - 1)
						continue;
					if(maze[r + 2, c] != 0){
						maze[r+2, c] = 0;
						maze[r+1, c] = 0;
						MazeDigger(maze, r + 2, c);
					}
					break;
					
			case 3: // east
					// check if 2 cells north is out of range
					if(c + 2 >= MazeWidth - 1)
						continue;
					if(maze[r, c + 2] != 0){
						maze[r, c + 2] = 0;
						maze[r, c + 1] = 0;
						MazeDigger(maze, r, c + 2);
					}
					break;
					
			case 4: // west
					// check if 2 cells north is out of range
					if(c - 2 <= 0)
						continue;
					if(maze[r, c - 2] != 0){
						maze[r, c - 2] = 0;
						maze[r, c - 1] = 0;
						MazeDigger(maze, r, c - 2);
					}
					break;
					
			}
		}
	}

	public static void Shuffle<T>(T[] array){
		var random = _random;
		for(int i = array.Length; i > 1; i--){
			// pick rand element
			int j = random.Next(i);
			//swap
			T tap = array[j];
			array[j] = array[i-1];
			array[i-1] = tap;
		}
	
	}

	// Update is called once per frame
	void FixedUpdate () {
	}
}
