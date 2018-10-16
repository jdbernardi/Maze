using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour {

	public GameObject player;
	public AudioSource ambientSounds;

	// Use this for initialization
	void Start () {
		player = Instantiate(player, new Vector3(-10f, 0.5f, 32f), Quaternion.identity) as GameObject;
		key.keyPickedUp = false;
		Item.totalCoinsPickedUp = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
