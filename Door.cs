using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
    // Create a boolean value called "locked" that can be checked in OnDoorClicked() 
    // Create a boolean value called "opening" that can be checked in Update() 
    public Animator anim;
    public GameObject door;
    public AudioClip lockedSound;
    public AudioClip openSound;


    public void OnDoorClicked() {
		anim = GetComponent<Animator>();
    	if(key.keyPickedUp == true){
			AudioSource.PlayClipAtPoint(openSound, new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z));
			anim.Play("OpenDoor", -1, 0f);
       	} else {
			AudioSource.PlayClipAtPoint(lockedSound, new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z));
			anim.Play("LockedDoor", -1, 0f);
       	}
        // If the door is clicked and unlocked
            // Set the "opening" boolean to true
        // (optionally) Else
            // Play a sound to indicate the door is locked
    }


}
