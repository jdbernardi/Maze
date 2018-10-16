using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinHUD : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		TextMesh textMesh = GetComponent<TextMesh>();
		if(Item.totalCoinsPickedUp > 0){
			textMesh.text = "Coins: " + Item.totalCoinsPickedUp;
		} else {
			textMesh.text = "";
		}
	}
}
