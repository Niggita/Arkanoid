using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelUIHandler : MonoBehaviour {

	[SerializeField]
	private GameObject WinPanel;

	[SerializeField]
	private GameObject LosePanel;

	[SerializeField]
	private GameObject InfoPanel;

	[SerializeField]
	private Text scoreText;

	[SerializeField]
	private Text livesText;

	[SerializeField]
	private Text infoText;

	public void ShowWinPanel(bool show){
		WinPanel.SetActive (show);
	}

	public void ShowLosePanel(bool show){
		LosePanel.SetActive (show);
	}

	public void ShowStartPanel(bool show){
		InfoPanel.SetActive (show);
	}

	public void ToMenu(){
		SoundsHandler.instance.PlayButtonSound ();
		SceneManager.LoadScene (0);
	}

	public void NextLevel(){
		SoundsHandler.instance.PlayButtonSound ();
		if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1){
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
		}else{
			SceneManager.LoadScene (0);
		}
	}

	public void SetScore(int score){
		scoreText.text = "Очков: " + score.ToString ();
	}

	public void SetLives(int lives){
		livesText.text = "Жизней: " + lives.ToString ();
	}

	public void SetStartMessage(){
		switch(SceneManager.GetActiveScene().buildIndex){
		case 1:{
				infoText.text = "Уровень 1: Петроградъ";
				break;
			}
		case 2:{
				infoText.text = "Уровень 2: Москва";
				break;
			}
		case 3:{
				infoText.text = "Уровень 3: Царицынъ";
				break;
			}
		}
	}
}
