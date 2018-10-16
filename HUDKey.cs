using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDKey : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TextMesh textMesh = GetComponent<TextMesh>();
		if(key.keyPickedUp == true){
			textMesh.text = "Key Found!\nTotal Coins: " + Item.totalCoinsPickedUp;
		} else if(key.keyPickedUp == false && Item.totalCoinsPickedUp > 0) {
			textMesh.text = "Total Coins: " + Item.totalCoinsPickedUp;
		} else {
			textMesh.text = "";
		}
	}
}
