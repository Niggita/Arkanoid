using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : InteractableObject {

	private int speedDivider;
	private Vector3 savedPosition;

	void Awake(){
		speedDivider = GameManager.instance.options.PlatformSpeed;
		savedPosition = transform.position;
	}

	void FixedUpdate(){
		if (play) {
			speed.x = Input.GetAxis ("Horizontal") / speedDivider;
			if (wallCollides && !(collidedSide == CollideSide.NONE)) {
				if ((collidedSide == CollideSide.LEFT && speed.x > 0) ||
				   (collidedSide == CollideSide.RIGHT && speed.x < 0)) {
					Move ();	
				} else {
					speed.x = -speed.x;
				}
			} else {
				Move ();
			}
		}
	}

	public void Reset(){
		transform.position = savedPosition;
		speed.x = 0f;
	} 
}
