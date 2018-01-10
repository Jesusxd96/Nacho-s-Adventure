using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectScreen : MonoBehaviour {
	public static LevelSelectScreen sharedInstance;
	public GameObject levelButtonPrefab;
	public GameObject levelButtonContainer;

	void Awake(){
		sharedInstance = this;
	}

	private void Start () {
		Sprite[] thumnails = Resources.LoadAll<Sprite> ("LevelsPreview");
		foreach (Sprite thumbnail in thumnails) {
			GameObject container = Instantiate (levelButtonPrefab) as GameObject;
			container.GetComponent<Image> ().sprite = thumbnail;
			container.transform.SetParent (levelButtonContainer.transform, false);

			string levelGeneratorName = thumbnail.name;
			//string backgroundGeneratorName = thumbnail.name;
			/*container.GetComponent<Button> ().onClick.AddListener (() => loadLevel (levelGeneratorName));*/

		}
	}

	/*public void loadLevel (string levelName){
		GameObject level = GameObject.FindGameObjectWithTag (levelName);
		GameObject other;
		if (GameObject.FindWithTag (levelName) || GameObject.FindWithTag ("MainGame")) {
			level.SetActive (true);
		} else {
			other = GameObject.FindGameObjectWithTag (other.tag);
			other.SetActive (false);
		}
		GameManager.sharedInstance.StartGame ();
	}*/

}