using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGuard : InteractableObject {

	protected override void Start (){
		base.Start ();
		float[] variants = { -0.05f, 0.05f };
		speed = new Vector2 (variants[Random.Range(0, 2)], variants[Random.Range(0, 2)]);
		gameObject.AddComponent <CircleCollider2D>();
		rBody.gravityScale = 0;
		rBody.freezeRotation = true;
	}
		
	void FixedUpdate(){
		Move ();
	}

	protected virtual void OnCollisionEnter2D(Collision2D col){
		base.OnCollisionEnter2D (col);

		if (wallCollides) {
			SoundsHandler.instance.PlayPlatformSound ();
			switch (collidedSide) {
			case CollideSide.RIGHT:{
					if(speed.x > 0)
						speed.x = -speed.x;
					break;
				}
			case CollideSide.DOWN:{
					if(speed.y < 0)
						speed.y = -speed.y;
					break;
				}
			case CollideSide.LEFT:{
					if(speed.x < 0)
						speed.x = -speed.x;
					break;
				}
			case CollideSide.TOP:{
					if(speed.y > 0)
					speed.y = -speed.y;
					break;
				}
			}
		}

		if (otherObjCollides) {
			if (col.gameObject.GetComponent<Lenin> () != null || col.gameObject.GetComponent<Platform> () != null) {
				Physics2D.IgnoreCollision (col.gameObject.GetComponent<Collider2D> (), gameObject.GetComponent<Collider2D> ());
				SoundsHandler.instance.PlayPlatformSound ();
			}
			else if (col.gameObject.GetComponent<Enemy> () != null) {
				Destroy (gameObject);
			} else {
				speed.x = -speed.x;
				speed.y = -speed.y;
			}
		}
	}

	public void SetImage(Sprite sprite){
		gameObject.AddComponent <SpriteRenderer>();
		GetComponent<SpriteRenderer> ().sprite = sprite;
	}
}
