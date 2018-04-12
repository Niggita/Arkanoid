using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lenin : InteractableObject {

	private FixedJoint2D platformJoint;
	public GameObject platform;

	private Vector3 savedPosition;

	private UnityEvent LeninDies;

	private float savedSpeed;

	void Awake(){
		savedPosition = transform.position;
	
		LeninDies = new UnityEvent ();
		LeninDies.AddListener (GameManager.instance.HeroDead);

		savedSpeed = GameManager.instance.options.LeninSpeed;
	}

	protected override void Start (){
		base.Start ();
		platformJoint = gameObject.AddComponent<FixedJoint2D> ();
		platformJoint.connectedBody = platform.GetComponent<Rigidbody2D>();
		platformJoint.enabled = true;
	}

	void FixedUpdate(){
		if (play) {
			if (Input.GetKeyDown (KeyCode.Space) && (speed.x > -0.01f && speed.x < 0.01f)) {
				platformJoint.enabled = false;
				speed = new Vector2(0f, savedSpeed);
			}
			Move ();
		}
	}  

	protected virtual void OnCollisionEnter2D(Collision2D col){
		base.OnCollisionEnter2D (col);
		
		if (wallCollides) {
			SoundsHandler.instance.PlayPlatformSound ();
			switch (collidedSide) {
			case CollideSide.RIGHT:{
					speed.x = -speed.x;
					break;
				}
			case CollideSide.DOWN:{
					LeninDies.Invoke ();
					break;
				}
			case CollideSide.LEFT:{
					speed.x = -speed.x;
					break;
				}
			case CollideSide.TOP:{
					speed.y = -speed.y;
					break;
				}
			}
		}

		else if (otherObjCollides) {
			if (col.gameObject.GetComponent<Platform>() == null) {
				speed.x = -speed.x;
				speed.y = -speed.y;
			} else {
				var currentPos = transform.position.x - platform.transform.position.x;
				var ang = currentPos / Mathf.Abs (platform.GetComponent<BoxCollider2D>().bounds.size.x / 2);
				speed.x = Mathf.Sin (ang);
				speed.y = Mathf.Cos (ang);
				speed = speed.normalized;
				speed.x /= (1f / savedSpeed);
				speed.y /= (1f / savedSpeed);
				if (speed.y < 0f)
					speed.y = -speed.y;
				platformJoint.enabled = true;
				platformJoint.connectedBody = col.collider.attachedRigidbody;
				Invoke ("Unjoint", 0.2f);
				SoundsHandler.instance.PlayPlatformSound ();
			}
		}
	}

	private void Unjoint(){
		platformJoint.enabled = false;
	}

	public void Reset(){
		transform.position = savedPosition;
		platformJoint.enabled = true;
		speed.x = 0f;
		speed.y = 0f;
	}
}
