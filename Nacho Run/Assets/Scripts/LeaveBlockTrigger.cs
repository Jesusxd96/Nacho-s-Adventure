using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveBlockTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider){
		//Debug.Log ("Destruye");
		LevelGenerator.sharedInstance.AddNewBlock ();
		LevelGenerator.sharedInstance.RemoveOldBlock ();
	}

}