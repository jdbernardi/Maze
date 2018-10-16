using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour 
{

    public GameObject poof;
    public GameObject item;
    public AudioClip sound;

    public static int totalCoinsPickedUp = 0;

    public void OnItemClicked() {
        // Instantiate the CoinPoof Prefab where this coin is located
        Vector3 itemPosition = this.transform.position;
        Debug.Log(item.name);
        AudioSource.PlayClipAtPoint(sound, new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z));
        Instantiate(poof, itemPosition, Quaternion.Euler(-90f, 0f, 0f));
        Destroy(item);
        // Make sure the poof animates vertically
        // Destroy this coin. Check the Unity documentation on how to use Destroy
    }

    public void CoinPickedUp(){
    	totalCoinsPickedUp++;
    	Debug.Log("Coins Picked Up: " + totalCoinsPickedUp);
    }

}
