using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	//Estas son las monedas
	private AudioSource source;
	private bool isCollected = false;

	public void ShowCoins(){
		this.GetComponent<SpriteRenderer> ().enabled = true;
		this.GetComponent<CapsuleCollider2D> ().enabled = true;
		isCollected = false;

	}
	public void HideCoins(){
		this.GetComponent<SpriteRenderer> ().enabled = false; // Esto hace que el SpriteRenderer se desactive y la moneda desaparezca
		this.GetComponent<CapsuleCollider2D>().enabled = false;//Esto desactiva el collider
	}

	public void CollectCoins(){
		AudioSource audio = GetComponent<AudioSource>(); //Igual no se si este de mas
		isCollected = true;
		audio.Play ();
		HideCoins ();
		//Notificar al Game manager que la moneda ha sido recogida
		GameManager.sharedInstance.CollectCoin();
	}

	public void OnTriggerEnter2D(Collider2D otherCollider){
		if(otherCollider.tag == "Player"){
			CollectCoins();
		}
	}
}