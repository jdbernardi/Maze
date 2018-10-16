using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public int speed;
	private CharacterController cc;
	public float toggleAngle = 30.0f;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController>();
	}

	void FixedUpdate(){
		Physics.IgnoreLayerCollision(8,9);
		Transform camera = GetComponent<Camera>().transform;
//		Debug.Log(camera.eulerAngles.x);
		if(camera.eulerAngles.x > 0.0f && camera.eulerAngles.x < toggleAngle){
			Vector3 forward = camera.TransformDirection(Vector3.forward);
			cc.SimpleMove(forward * speed);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
