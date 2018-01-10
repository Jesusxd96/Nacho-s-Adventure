using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopMenu : MonoBehaviour {

	//private int index;
	public static ShopMenu sharedInstance;
	public GameObject shopButtonPrefab;
	public GameObject shopItemContainer;
	private int indexCharacter;
	private string[] skinName = new string[10];//La variable temporal para el guardado de nombre de la skin

	public Text coinsText;

	void Awake(){
		sharedInstance = this;
	}

	void Update(){//Utilizado para actualizar el numero de monedas en el menu de inicio
		coinsText.text= PlayerPrefs.GetInt("Coins").ToString();
	}

	void Start(){
		int indexCharacter = 0;
		Sprite[] items = Resources.LoadAll<Sprite> ("Previews");
		Sprite[] characters = Resources.LoadAll<Sprite> ("Characters");

		int i = indexCharacter;
		foreach (Sprite item in items) {
			//Debug.Log (item);
			GameObject container = Instantiate (shopButtonPrefab) as GameObject;
			container.GetComponent<Image>().sprite = item;
			string skin = container.GetComponent<Image>().sprite.name;
			container.transform.SetParent (shopItemContainer.transform, false);
			//container.GetComponent<Button> ().onClick.AddListener (() => container.GetComponent<Image> ().sprite.name);
			container.GetComponent<Button> ().onClick.AddListener (() => ChangeSkin (skin));
			indexCharacter++;
		}
	}
	void ChangeSkin(string skinName){
		//Debug.Log ("Skin: " + skinName);
		//PlayerPrefs.SetString("CurrentSkin",skinName);
		//GameManager.sharedInstance.Save ();
		PlayerController.sharedInstance.ChangeSkin (skinName);
	}
}