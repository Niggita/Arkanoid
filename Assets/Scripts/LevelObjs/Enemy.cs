using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : InteractableObject {

	private int moveTime;
	private UnityEvent deathEvent;

	void Awake(){
		moveTime = GameManager.instance.options.EnemiesSpeed;
		deathEvent = new UnityEvent ();
		deathEvent.AddListener (GameManager.instance.AddScore);
	}

	void OnDisable(){
		StopAllCoroutines ();
	}

	protected override void Start (){
		base.Start ();
		gameObject.AddComponent <CircleCollider2D>();
		rBody.isKinematic = true;
		speed = new Vector2 (0, -gameObject.GetComponent<CircleCollider2D>().radius * 2);
		//StartCoroutine (MoveEnemy());
	}

	public void SetImage(Sprite sprite){
		gameObject.AddComponent<SpriteRenderer> ();
		gameObject.GetComponent<SpriteRenderer> ().sprite = sprite;
	}

	protected virtual void OnCollisionEnter2D(Collision2D col){
		base.OnCollisionEnter2D (col);
		if (col.gameObject.GetComponent<Lenin> () != null ||
			col.gameObject.GetComponent<RedGuard>() != null) {
			Die ();
			SoundsHandler.instance.PlayEnemySound ();
		}
	}

	private void Die(){
		Destroy (gameObject);
		deathEvent.Invoke ();
	}

	private IEnumerator MoveEnemy(){
		while (true) {
			yield return new WaitForSeconds (5f);
			Move ();
		}
	}

	public override bool Play {
		set {
			base.Play = value;
			if (value)
				StartCoroutine (MoveEnemy());
		}
	}
}