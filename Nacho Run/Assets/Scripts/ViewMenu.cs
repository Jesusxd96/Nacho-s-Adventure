using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewMenu : MonoBehaviour {
	public static ViewMenu sharedInstance;
	public Text coinsLabel;

	void Awake(){
		sharedInstance = this;
	}
	void Update(){//Utilizado para actualizar el numero de monedas en el menu de inicio
		coinsLabel.text= PlayerPrefs.GetInt("Coins").ToString();
	}

}