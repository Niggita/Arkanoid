using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLevelSetup : MonoBehaviour {

	[SerializeField]
	private MenuUiHandler UIHandler;

	void Awake(){
		Invoke ("SetParams", 0.01f);	
	}

	private void SetParams(){
		UIHandler.SetSliderMusicValue (SoundsHandler.instance.GetMusicVolume());
		UIHandler.SetSliderHardnessValue (GameManager.instance.Hardness);
	}
}
