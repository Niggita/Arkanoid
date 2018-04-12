using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayLevelSetup : MonoBehaviour {

	[SerializeField]
	private List<Sprite> EnemySprites;

	[SerializeField]
	private Sprite GuardSprite;

	[SerializeField]
	private int RowsOfEnemies;

	[SerializeField]
	private GameObject enemyContainer;

	[SerializeField]
	private float offsetBetweenEnemies;

	public Lenin lenin;
	public Platform platform;
	public LevelUIHandler UIHandler;

	private UnityEvent WinEvent;
	private UnityEvent LoseEvent;

	private int enemiesLineLength;

	private bool guardsCalled;

	private bool play;

	void Awake(){
		WinEvent = new UnityEvent ();
		WinEvent.AddListener (GameManager.instance.Win);

		LoseEvent = new UnityEvent ();
		LoseEvent.AddListener (GameManager.instance.Lose);
	}

	void FixedUpdate(){
		if (play) {
			if (enemyContainer.GetComponentInChildren<Enemy> () == null)
				WinEvent.Invoke ();
			foreach (Enemy enemy in enemyContainer.GetComponentsInChildren<Enemy>()) {
				if (enemy.transform.position.y < platform.transform.position.y) {
					LoseEvent.Invoke ();
				}
			}

			if (!guardsCalled && Input.GetKeyDown (KeyCode.Alpha1)) {
				CallGuards (enemiesLineLength);
				guardsCalled = true;
			}
		}
	}

	public void Initialize(int width){
		enemiesLineLength = width;
		SetEnemies ();
	}

	public void Play (bool isPlaying){
		play = isPlaying;
		foreach (InteractableObject obj in GameObject.FindObjectsOfType(typeof(InteractableObject))) {
			obj.Play = isPlaying;
		}
	}

	private void SetEnemies(){
		UIHandler.ShowStartPanel (false);
		int columns = (int)(enemiesLineLength / EnemySprites [0].bounds.size.x);
		GameObject enemy = new GameObject ("Enemy");
		enemy.AddComponent<Enemy> ();
		for (int i = 0; i < columns; i++) {
			for (int j = 0; j < RowsOfEnemies; j++) {
				GameObject instance = Instantiate (enemy, 
					new Vector3 (enemyContainer.transform.position.x + i * (EnemySprites [0].bounds.size.x + offsetBetweenEnemies),
						enemyContainer.transform.position.y + j * (EnemySprites[0].bounds.size.y + offsetBetweenEnemies),
						enemyContainer.transform.position.z),
					Quaternion.identity);
				instance.GetComponent<Enemy>().SetImage(EnemySprites[Random.Range(0, EnemySprites.Count)]);
				instance.transform.SetParent (enemyContainer.transform);
			}
		}
		Destroy (enemy);
	}

	private void CallGuards(int width){
		int columns = (int)(width / EnemySprites [0].bounds.size.x);
		GameObject guard = new GameObject ("Guard");
		guard.AddComponent<RedGuard> ();
		for (int i = 0; i < columns; i++) {
			GameObject instance = Instantiate (guard, 
				new Vector3 (enemyContainer.transform.position.x + i * (GuardSprite.bounds.size.x + offsetBetweenEnemies),
					platform.transform.position.y + 1,
					platform.transform.position.z),
				Quaternion.identity);
			instance.GetComponent<RedGuard>().SetImage(GuardSprite);
			instance.transform.SetParent (enemyContainer.transform);
			instance.GetComponent<RedGuard> ().Play = true;
		}
		Destroy (guard);
	}
}
