using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
	menu,
	inTheGame,
	gameOver,
	options,
	levelSelect,
	shop,
	storyText
}

public class GameManager : MonoBehaviour {

	public static GameManager sharedInstance;

	public GameState currentGameState = GameState.menu; 
	public Canvas menuCanvas; //Canvas del menu principal
	public Canvas optionsMenu;
	public Canvas levelSelectMenu;
	public Canvas shopMenu;
	public Canvas gameCanvas;
	public Canvas gameOverCanvas;
	public Canvas loreCanvas;


	public int collectedCoins=0;
	public float highScore = 0;
	public int skinAvailability = 1;
	public string currentSkin="";

	void Awake(){
		sharedInstance = this;

		if(PlayerPrefs.HasKey("CurrentSkin")){
			//Blah
			currentSkin = PlayerPrefs.GetString("CurrentSkin");
			highScore = PlayerPrefs.GetFloat ("highscore");
			collectedCoins = PlayerPrefs.GetInt ("Coins");
			skinAvailability = PlayerPrefs.GetInt ("skinAvailability");
		} else{
			Save();
		}
	}

	public void Save(){
		PlayerPrefs.SetString("CurrentSkin",currentSkin);
		PlayerPrefs.SetInt ("skinAvailability", skinAvailability);
	}

	void Start(){

		currentGameState = GameState.menu;
		menuCanvas.enabled = true;
		gameCanvas.enabled = false;
		gameOverCanvas.enabled = false;
		optionsMenu.enabled = false;
		shopMenu.enabled = false;
		loreCanvas.enabled = false;
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

	public void settingsMenu(){
		ChangeGameState (GameState.options);	
	}

	public void enterShop(){
		ChangeGameState (GameState.shop);	
	}
	public void exitShop(){
		ChangeGameState (GameState.menu);
	}
	public void levelSelect(){
		ChangeGameState (GameState.levelSelect);	
	}
	public void checkLore(){
		//if (collectedCoins <= 30) {
			ChangeGameState (GameState.storyText);
		//}else{
			//Necesitas 30 tacos para poder ver el transfondo

		//}
	}

	void ChangeGameState(GameState newGameState){

		if (newGameState == GameState.menu) {
			//La escena de unity debera mostrar el menu principal
			menuCanvas.enabled = true;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
			optionsMenu.enabled = false;
			levelSelectMenu.enabled = false;
			shopMenu.enabled = false;
			loreCanvas.enabled = false;
			PlayerController.sharedInstance.animator.SetBool("isAlive",true);
			PlayerController.sharedInstance.animator.SetBool("isIdle",false);
		} else if (newGameState == GameState.inTheGame) {
			//La escena de unity debe configurare para mostrar el juego en si
			menuCanvas.enabled = false;
			gameCanvas.enabled = true;
			gameOverCanvas.enabled = false;
			optionsMenu.enabled = false;
			levelSelectMenu.enabled = false;
			shopMenu.enabled = false;
			loreCanvas.enabled = false;
		} else if (newGameState == GameState.gameOver) {
			//La logica para que la escena debe mostrar la pantalla de fin de la partida
			menuCanvas.enabled = false;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = true;
			optionsMenu.enabled = false;
			levelSelectMenu.enabled = false;
			shopMenu.enabled = false;
			loreCanvas.enabled = false;
		}
		else if (newGameState == GameState.options) {
			//La logica para que la escena debe mostrar la pantalla de fin de la partida
			menuCanvas.enabled = false;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
			optionsMenu.enabled = true;
			levelSelectMenu.enabled = false;
			shopMenu.enabled = false;
			loreCanvas.enabled = false;
		}
		else if (newGameState == GameState.shop) {
			//La logica para que la escena debe mostrar la pantalla de fin de la partida
			menuCanvas.enabled = false;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
			optionsMenu.enabled = false;
			levelSelectMenu.enabled = false;
			loreCanvas.enabled = false;
			shopMenu.enabled = true;
			PlayerController.sharedInstance.rigidBody.velocity = new Vector2 (0,0);
			PlayerController.sharedInstance.transform.position = PlayerController.sharedInstance.startPosition;
			PlayerController.sharedInstance.animator.SetBool("isAlive",false);
			PlayerController.sharedInstance.animator.SetBool("isIdle",true);
		}
		else if (newGameState == GameState.levelSelect) {
			//La logica para que la escena debe mostrar la pantalla de fin de la partida
			menuCanvas.enabled = false;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
			optionsMenu.enabled = false;
			levelSelectMenu.enabled = true;
			shopMenu.enabled = false;
			loreCanvas.enabled = false;
		}
		else if (newGameState == GameState.storyText) {
			//La logica para que la escena debe mostrar la pantalla de fin de la partida
			menuCanvas.enabled = false;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = false;
			optionsMenu.enabled = false;
			levelSelectMenu.enabled = true;
			shopMenu.enabled = false;
			loreCanvas.enabled = true;
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