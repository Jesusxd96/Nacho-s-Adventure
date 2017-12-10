using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Para que conozca la clase Text
public class ViewGameOver : MonoBehaviour {

	public static ViewGameOver sharedInstance;
	public Text coinsLabel;
	public Text scoreLabel;
	// Use this for initialization
	void Awake(){
		sharedInstance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateUI(){
		if (GameManager.sharedInstance.currentGameState == GameState.gameOver) {
			coinsLabel.text = GameManager.sharedInstance.collectedCoins.ToString ();
			scoreLabel.text = PlayerController.sharedInstance.GetDistance().ToString ("f0");
			GameManager.sharedInstance.collectedCoins = 0;//Resetea el contador de las monedas cada que se inicia el juego
			ViewInGame.sharedInstance.coinsLabel.text = GameManager.sharedInstance.collectedCoins.ToString ();
			//Script para actualizar las monedas del jugador, o mas bien, los tacos

		}
	}
}
