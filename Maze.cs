using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

	[System.Serializable]
	public class Cell {
		public bool visited;
		public GameObject north;//1
		public GameObject south;//4
		public GameObject east;//3
		public GameObject west;//2
	}

	public GameObject wall;
	public GameObject pillar;
	public GameObject coin;
	public GameObject key;
	public GameObject mazeFloor;
	public GameObject player;
	public float wallLength;
	public Vector3 startPos;
	public int xSize;
	public int ySize;
	public Cell[] cells;
	public int currentCell = 0;

	private Vector3 initialPosition;
	private Stack<Cell> cellStack;
	private GameObject wallHolder;
	private int totalCells;
	private int visitedCells;
	private bool startedBuilding = false;
	private int currentNeighbor;
	private List<int> lastCells;
	private int backingUp = 0;
	private int wallToBreak = 0;



	void Start () {
		SetStartPos();
		CreateWalls();
	}

	void CreateWalls() {
		wallHolder = new GameObject ();
		wallHolder.name = "Maze";
		initialPosition = new Vector3 ( 1f, 0.5f, 0f);
		Vector3 currentPos = initialPosition;

		GameObject tempWall;

		for (int i = 0; i <= ySize; i++){
			for(int j = 0; j < xSize; j++){
				currentPos = new Vector3 (initialPosition.x + (j*wallLength), initialPosition.y, initialPosition.z + (i*wallLength));
				tempWall = Instantiate(wall, currentPos, Quaternion.identity) as GameObject;
				tempWall.transform.parent = wallHolder.transform;

			}
		}

		initialPosition.x = 0;
		currentPos = initialPosition;
		Debug.Log("Creating Y Walls");
		for (int i = 0; i < ySize; i++){
			for(int j = 0; j <= ySize; j++){
				currentPos = new Vector3 (initialPosition.x + (j*wallLength), initialPosition.y, initialPosition.z + (i*wallLength) + 1f);
				tempWall = Instantiate(wall, currentPos, Quaternion.Euler(0f, 90f, 0f)) as GameObject;
				tempWall.transform.parent = wallHolder.transform;

			}
		}
		createCells();
		CreateMaze();
	}


	void createCells() {
		lastCells = new List<int> ();
		lastCells.Clear ();
		GameObject[] allWalls; 
		int children = wallHolder.transform.childCount;
		int eastWestProcess = 0;
		int childProcess = 0;
		int termCount = 0;

		allWalls = new GameObject[children];
		cells = new Cell[xSize * ySize];

		// get all the children
		for(int i = 0; i < children; i++){
			allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
		}
		for(int cellProcess = 0; cellProcess < cells.Length; cellProcess++){
			cells[cellProcess] = new Cell ();
			cells[cellProcess].south = allWalls[eastWestProcess];
			cells[cellProcess].west = allWalls[childProcess + (xSize+1)*ySize];
			if( termCount == xSize) {
				eastWestProcess += 2;
				termCount = 0;
			} else {
				eastWestProcess++;
				termCount++;
				childProcess++;
				cells[cellProcess].north = allWalls[eastWestProcess];
				cells[cellProcess].east = allWalls[(childProcess + (xSize+1)*ySize) + xSize - 1];
			}

		}
	
	}

	void CreateMaze(){
		while(visitedCells < totalCells){
			if(startedBuilding){
				GetNeighbor();
				if(cells[currentNeighbor].visited == false &&  cells[currentCell].visited == true){
					BreakWall();
					cells[currentNeighbor].visited = true;
					visitedCells++;
					lastCells.Add(currentCell);
					currentCell = currentNeighbor;
					if(lastCells.Count > 0){
						backingUp = lastCells.Count - 1;
					}
				}
			} else {
				currentCell = Random.Range(0, totalCells);
				cells[currentCell].visited = true;
				visitedCells++;
				startedBuilding = true;
			}
		}

	}

	void BreakWall(){
		Debug.Log("WallToBreak: " + cells[currentCell]);
		switch (wallToBreak) {
			case 1 : Destroy(cells[currentCell].north); break;
			case 2 : Destroy(cells[currentCell].west); break;
			case 3 : Destroy(cells[currentCell].east); break;
			case 4 : Destroy(cells[currentCell].south); break;
		}
	}

	void GetNeighbor(){
		
		int length = 0;
		int[] neighbors = new int[4];
		int check = 0;
		int[] connectingWall = new int[4];

		check = ((currentCell + 1)/(xSize));
		check -= 1;
		check *= ySize;
		check += ySize;
		//north
		if(currentCell + ySize < totalCells) {
			if(cells[currentCell + xSize].visited == false){
				neighbors[length] = currentCell + xSize;
				connectingWall[length] = 1;
				length++;
			}
		}
		//west
		if(currentCell - 1 >= 0 && currentCell != check){
			if(cells[currentCell - 1].visited == false){
				neighbors[length] = currentCell - 1;
				connectingWall[length] = 2;
				length++;
			}
		}
		//east
		if(currentCell + 1 < totalCells && (currentCell + 1) != check){
			if(cells[currentCell + 1].visited == false){
				neighbors[length] = currentCell + 1;
				connectingWall[length] = 3;
				length++;
			}
		}

		//south
		if(currentCell - ySize >= 0){
			if(cells[currentCell].visited == false){
				neighbors[length] = currentCell - xSize;
				connectingWall[length] = 4;
				length++;
			}
		}

		if(length != 0){
			int theChosenOne = Random.Range(0, length);
			currentNeighbor = neighbors[theChosenOne];
			wallToBreak = connectingWall[theChosenOne];
		} else {
			if(backingUp > 0){
				currentCell = lastCells[backingUp];
				backingUp--;
			}
		}

	}

	void SetStartPos(){
//		int xpos = RandomOddNumber(xSize);
//		int zpos = RandomOddNumber(ySize);
//		startPos = new Vector3(xpos, 0.5f, zpos);
		totalCells = xSize * ySize;
		currentCell = Random.Range(0, totalCells);
	}

	void AddPlayer(){
		Instantiate(player, startPos, Quaternion.identity);
	}

	void DestroyWall(){
		wall = GameObject.Find("1:2") as GameObject;
		Destroy(wall);
	}

	int RandomOddNumber(int num){
		int newNum = Random.Range(0, num + 1);
		while(newNum % 2 == 0){
			newNum = Random.Range(0, num + 1);
		}
		return newNum;
	}


	void Update () {
		
	}
}
