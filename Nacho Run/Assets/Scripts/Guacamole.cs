using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guacamole : MonoBehaviour {

	private bool isCollected = false;

	public void ShowGuacamole(){
		this.GetComponent<SpriteRenderer> ().enabled = true;
		this.GetComponent<CircleCollider2D> ().enabled = true;
		isCollected = false;

	}
	public void HideGuacamole(){
		this.GetComponent<SpriteRenderer> ().enabled = false; // Esto hace que el SpriteRenderer se desactive y la moneda desaparezca
		this.GetComponent<CircleCollider2D>().enabled = false;//Esto desactiva el collider
	}

	public void CollectGuacamole(){
		isCollected = true;
		HideGuacamole ();
		//Notificar al Game manager que el guacamole ha sido recogido
		GameManager.sharedInstance.CollectGuacamole();
	}

	public void OnTriggerEnter2D(Collider2D otherCollider){
		if(otherCollider.tag == "Player"){
			CollectGuacamole();
		}
	}

}