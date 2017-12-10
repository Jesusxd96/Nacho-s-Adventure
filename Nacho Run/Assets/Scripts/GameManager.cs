using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
	menu,
	inTheGame,
	gameOver
}

public class GameManager : MonoBehaviour {

	public static GameManager sharedInstance;

	public GameState currentGameState = GameState.menu;

	public Canvas menuCanvas; //Canvas del menu principal
	public Canvas gameCanvas;
	public Canvas gameOverCanvas;

	public int collectedCoins=0;

	void Awake(){
		sharedInstance = this;
	}

	void Start(){
		currentGameState = GameState.menu;
		menuCanvas.enabled = true;
		gameCanvas.enabled = false;
		gameOverCanvas.enabled = false;
	}

	void Update(){
		/*if(Input.GetKeyDown(KeyCode.S)){
			if (currentGameState != GameState.inTheGame) {
				StartGame ();
			}
		}*/
	}

	// Usaremos esto para empezar la partida
	public void StartGame () {
		PlayerController.sharedInstance.StartGame ();
		LevelGenerator.sharedInstance.GenerateInitialBlocks ();
		BackgroundGenerator.sharedInstance.GenerateInitialBlocks ();
		ChangeGameState (GameState.inTheGame);
		//currentGameState = GameState.inTheGame;
		ViewInGame.sharedInstance.UpdateHighScoreLabel();
	}

	//Cuando el jugador muere
	public void Gameover(){
		ChangeGameState (GameState.gameOver);
		LevelGenerator.sharedInstance.RemoveAllBlocks ();
		BackgroundGenerator.sharedInstance.RemoveAllBlocks ();
		ViewGameOver.sharedInstance.UpdateUI ();
	}

	//Para regresar al menu principal si el jugador lo decide
	public void BackToMainMenu(){
		ChangeGameState (GameState.menu);
	}

	void ChangeGameState(GameState newGameState){

		if (newGameState == GameState.menu) {
			//La escena de unity debera mostrar el menu principal
			menuCanvas.enabled = true;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
		} else if (newGameState == GameState.inTheGame) {
			//La escena de unity debe configurare para mostrar el juego en si
			menuCanvas.enabled = false;
			gameCanvas.enabled = true;
			gameOverCanvas.enabled = false;
		} else if (newGameState == GameState.gameOver) {
			//La logica para que la escena debe mostrar la pantalla de fin de la partida
			menuCanvas.enabled = false;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = true;
		}
		currentGameState = newGameState;
	}

	public void CollectCoin (){
		collectedCoins++;
		//Debug.Log ("Monedas recojidas: " + collectedCoins);
		ViewInGame.sharedInstance.UpdateCoinsLabel ();
	}

	public void CollectGuacamole(){
		StartCoroutine (guacaTime());

	}
	public IEnumerator guacaTime(){
		PlayerController.sharedInstance.runningSpeed = 15.0f;
		yield return new WaitForSecondsRealtime (5);
		PlayerController.sharedInstance.runningSpeed = 9.0f;
	}
}