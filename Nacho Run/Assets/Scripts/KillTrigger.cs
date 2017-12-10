using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D theObject){
		if (theObject.tag == "Player") {
			PlayerController.sharedInstance.KillPlayer();
			//Debug.Log ("El conejo ha muerto alv!");
		}

	}

}