using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveBlockTrigger_Backgrounds : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider){
		//Debug.Log ("Destruye");
		BackgroundGenerator.sharedInstance.AddNewBlock ();
		BackgroundGenerator.sharedInstance.RemoveOldBlock ();
	}

}