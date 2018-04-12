using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUiHandler : MonoBehaviour {

	[SerializeField]
	private GameObject MainPanel;

	[SerializeField]
	private GameObject OptsPanel;

	[SerializeField]
	private Slider MusicSlider;

	[SerializeField]
	private Slider HardnessSlider;
 
	public void Play(){
		UnityEngine.SceneManagement.SceneManager.LoadScene 
			(UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void Opts(){
		SoundsHandler.instance.PlayButtonSound ();
		OptsPanel.SetActive (true);
		MainPanel.SetActive (false);
	}

	public void Exit(){
		Application.Quit ();
	}

	public void CloseOpts(){
		SoundsHandler.instance.PlayButtonSound ();
		OptsPanel.SetActive (false);
		MainPanel.SetActive (true);
	}

	public void SoundValue(Slider slider){
		SoundsHandler.instance.PlayButtonSound ();
		SoundsHandler.instance.SetVolume (slider.value);
	}

	public void GameHardness(Slider slider){
		SoundsHandler.instance.PlayButtonSound ();
		GameManager.instance.Hardness = (int)slider.value;
	} 

	public void SetSliderMusicValue(float value){
		MusicSlider.value = value;
	}

	public void SetSliderHardnessValue(int value){
		HardnessSlider.value = value;
	}
}
