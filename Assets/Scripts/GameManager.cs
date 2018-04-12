using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Options{
	public float LeninSpeed;
	public int PlatformSpeed;
	public int EnemiesSpeed;
}

public class GameManager : MonoBehaviour {

	public enum Level{
		MENU,
		GAME
	}

	[SerializeField]
	private int lives;
	[SerializeField]
	private int pointsPerEnemy;

	[SerializeField]
	private int enemyRowLength;
	[SerializeField]
	private float startDelay;

	private int score;
	private int actualLives;
	private int hardness;

	public Options options;

	public static GameManager instance = null;

	private Level level;

	private GameObject LevelSetup;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);
		Hardness = 1;
		actualLives = lives;
	}

	void Start(){
		CheckMusic ();
	}

	void OnLevelWasLoaded(){
		if (GameObject.FindGameObjectWithTag ("Game") != null) {
			level = Level.GAME;
			LevelSetup = GameObject.FindGameObjectWithTag ("Game");
			LevelSetup.GetComponent<PlayLevelSetup> ().Initialize (enemyRowLength);
			LevelSetup.GetComponent<PlayLevelSetup> ().UIHandler.SetStartMessage ();
			LevelSetup.GetComponent<PlayLevelSetup> ().UIHandler.ShowStartPanel (true);
			LevelSetup.GetComponent<PlayLevelSetup> ().UIHandler.SetScore(score);
			LevelSetup.GetComponent<PlayLevelSetup> ().UIHandler.SetLives (lives);
			Invoke ("StartLevel", startDelay);
		} else {
			actualLives = lives;
			score = 0;
			level = Level.MENU;
		}
		CheckMusic ();
	}

	private void StartLevel(){
		LevelSetup.GetComponent<PlayLevelSetup> ().UIHandler.ShowStartPanel (false);
		LevelSetup.GetComponent<PlayLevelSetup> ().Play (true);
	}

	private void CheckMusic(){
		if (level == Level.MENU) {
			SoundsHandler.instance.PlayMenuMusic();
		} else if (level == Level.GAME) {
			SoundsHandler.instance.PlayGameMusic ();;
		}
	}

	public void HeroDead(){
		LevelSetup.GetComponent<PlayLevelSetup> ().lenin.Reset ();
		LevelSetup.GetComponent<PlayLevelSetup> ().platform.Reset ();
		actualLives--;
		if (actualLives < 0)
			Lose ();
		LevelSetup.GetComponent<PlayLevelSetup> ().UIHandler.SetLives (actualLives);
	}

	public void Lose(){
		LevelSetup.GetComponent<PlayLevelSetup> ().UIHandler.ShowLosePanel(true);
		LevelSetup.GetComponent<PlayLevelSetup> ().Play (false);
	}

	public void Win(){
		LevelSetup.GetComponent<PlayLevelSetup> ().UIHandler.ShowWinPanel(true);		
		LevelSetup.GetComponent<PlayLevelSetup> ().Play (false);
	}

	public void AddScore(){
		score += pointsPerEnemy;
		LevelSetup.GetComponent<PlayLevelSetup> ().UIHandler.SetScore(score);
	}

	public int Hardness{
		set{
			hardness = value; 
			switch (value) {
			case 0:{
					options.LeninSpeed = 0.05f;
					options.EnemiesSpeed = 30;
					options.PlatformSpeed = 3;
					break;
				}
			case 1:{
					options.LeninSpeed = 0.1f;
					options.EnemiesSpeed = 20;
					options.PlatformSpeed = 6;
					break;
				}
			case 2:{
					options.LeninSpeed = 0.2f;
					options.EnemiesSpeed = 10;
					options.PlatformSpeed = 9;
					break;
				}
			}}
		get{ return hardness;}
	}
}
