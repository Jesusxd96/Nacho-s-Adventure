using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewMenu : MonoBehaviour {
	public static ViewMenu sharedInstance;
	//public int totalCollectedCoins;
	//public Text coinsLabel;

	void Awake(){
		sharedInstance = this;
	}

	public void UpdateCoinsLabel(){//Actualiza el numero de monedas en el menu de inicio
		
	}
		
}