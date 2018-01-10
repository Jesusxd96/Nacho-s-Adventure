using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	//Estas son las monedas
	public static Collectable sharedInstance;
	public int coins;
	private AudioSource source;
	private bool isCollected = false;

	// Use this for initialization
	void Awake ()
	{
		sharedInstance = this;
		coins = PlayerPrefs.GetInt ("Coins");
	}

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
		coins = PlayerPrefs.GetInt ("Coins");
		PlayerPrefs.SetInt ("Coins", coins + 1);
		//Debug.Log (PlayerPrefs.GetInt("Coins"));
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