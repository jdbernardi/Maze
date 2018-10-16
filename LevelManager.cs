using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject levelManager;

	public void LoadLevel(string name)
	{
		Debug.Log(this.name);
		if(this.name == "signLarge"){
			MyMaze.MazeWidth = 19;
			MyMaze.MazeHeight = 19;
		} else if (this.name == "signMedium"){
			MyMaze.MazeWidth = 13;
			MyMaze.MazeHeight = 13;
		} else if (this.name == "signSmall") {
			MyMaze.MazeWidth = 9;
			MyMaze.MazeHeight = 9;
		} 

		Application.LoadLevel(name);

	}


}
