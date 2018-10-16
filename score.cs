using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

	public Text scoreBoard;
	public AudioClip backgroundSound;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		scoreBoard.text = Item.totalCoinsPickedUp + " out of " + MyMaze.maxCoins;
	}
}
