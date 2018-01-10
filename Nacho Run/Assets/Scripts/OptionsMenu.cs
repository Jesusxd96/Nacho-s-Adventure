using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class OptionsMenu : MonoBehaviour {

	public AudioMixer audioMixer;

	public void backToMenu(){
		GameManager.sharedInstance.BackToMainMenu();
	}
	public void SetVolume(float volume){
		audioMixer.SetFloat ("Volume",volume);
		//PlayerPrefs.Save (audioMixer);
	}

	public void SetQuality(int qualityIndex){
		QualitySettings.SetQualityLevel (qualityIndex);
		//PlayerPrefs.Save (QualitySettings);
	}

}