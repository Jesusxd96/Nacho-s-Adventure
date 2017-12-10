using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;

public class PlayerController : MonoBehaviour {

	public static PlayerController sharedInstance;
	public float Ray = 1.74f;
	public float jumpForce = 15.0f;
	public float runningSpeed = 8.0f;
	private Rigidbody2D rigidBody;
	public LayerMask groundLayerMask;
	public Animator animator;
	private string highScoreKey = "highscore";
	private string totalCoinsKey = "totalCoins";
	//public int totalCoins;

	private Vector3 startPosition;//Posicion de arranque en 3 dimensiones

	void Awake(){
		animator.SetBool("isAlive",true);
		sharedInstance = this;
		rigidBody = GetComponent<Rigidbody2D> ();
		startPosition = this.transform.position;
	}


	public void StartGame () {
		AudioSource audio = GetComponent<AudioSource>(); //Igual no se si este de mas
		rigidBody.velocity = new Vector2 (0,0);
		this.transform.position = startPosition;
		animator.SetBool("isAlive",true);
		audio.Play ();
	}

	void FixedUpdate(){//Se ejecuta cada determinado tiempo
		if (GameManager.sharedInstance.currentGameState == GameState.inTheGame) {
			if (rigidBody.velocity.x < runningSpeed) {
				rigidBody.velocity = new Vector2 (runningSpeed, rigidBody.velocity.y);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.sharedInstance.currentGameState == GameState.inTheGame) {
			if (Input.GetMouseButtonDown(0)) {
				///Debug.Log (groundLayerMask.value); Su valor es
				//El jugador podra saltar con Space o Click izquierdo!
				//Debug.Log ("Boton izquierdo del raton pulsado!");
				Jump ();
			}
			animator.SetBool ("isGrounded", IsOnTheFloor ());
		}
	}

	void Jump(){
		if (IsOnTheFloor ()) {
			rigidBody.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);//Vector 2  sirve para up/down/left/right
		}
	}
		
	bool IsOnTheFloor(){
		if (Physics2D.Raycast (this.transform.position, Vector2.down, Ray, groundLayerMask.value)) {
			return true;
		} else {
			return false;
		}
	}

	public void KillPlayer(){
		//GetCoins ();//Se llama la funcion de guardado de monedas/tacos NO SIRVE :C
		AudioSource audio = GetComponent<AudioSource>(); //Igual no se si este de mas
		GameManager.sharedInstance.Gameover (); // Se llama directamente al METODO de gameover para notificar el fin de la partida
		audio.Stop();
		animator.SetBool("isAlive",false);
		runningSpeed = 9.0f;//Esto resetea la velocidad del jugador si este llegase a morir

		if (PlayerPrefs.GetFloat (highScoreKey, 0) < this.GetDistance ()) {
			PlayerPrefs.SetFloat(highScoreKey,this.GetDistance());
		}
	}

	public float GetDistance(){
		float distanceTraveled = Vector2.Distance (new Vector2(startPosition.x,0),//punto inicial
			new Vector2(this.transform.position.x,0));//Distancia viajada
		
		return distanceTraveled;
	}

	/*public void GetCoins(){
		PlayerPrefs.GetInt (totalCoinsKey, totalCoins);
		PlayerPrefs.SetInt (totalCoinsKey, (totalCoins + GameManager.sharedInstance.totalCollectedCoins));
		Debug.Log ("Las monedas de Nacho son: " + totalCoins);
	}

	public void SpendCoins(){
		//Para gastar dinero de las skins
	}*/
}