using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject :MonoBehaviour {

	public enum CollideSide{
		LEFT,
		RIGHT,
		TOP,
		DOWN,
		NONE
	}

	protected bool play;

	protected Vector2 speed;
	protected int health;

	protected bool wallCollides;
	protected bool otherObjCollides;

	protected Rigidbody2D rBody;

	protected CollideSide collidedSide;

	protected virtual void Start(){
		if (GetComponent<Rigidbody2D> () == null)
			gameObject.AddComponent<Rigidbody2D> ();
		rBody = GetComponent<Rigidbody2D> ();
		collidedSide = CollideSide.NONE;
	}

	protected void Move(){
		if (play) {
			rBody.MovePosition (new Vector3 (transform.position.x + speed.x, 
				transform.position.y + speed.y,
				transform.position.z));	
		}
	}

	protected virtual void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Bounds") {
			wallCollides = true;
			if (col.gameObject.name == "Left") {
				collidedSide = CollideSide.LEFT;
			}
			else if (col.gameObject.name == "Right") {
				collidedSide = CollideSide.RIGHT;
			}
			else if (col.gameObject.name == "Down") {
				collidedSide = CollideSide.DOWN;
			}
			else if (col.gameObject.name == "Top") {
				collidedSide = CollideSide.TOP;
			}
		}
		if (col.gameObject.GetComponent<InteractableObject> () != null) {
			otherObjCollides = true;
		}
	}

	protected virtual void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "Bounds") {
			wallCollides = false;
			collidedSide = CollideSide.NONE;

		}
		if (col.gameObject.GetComponent<InteractableObject> () != null) {
			otherObjCollides = false;
		}
	}

	public Vector2 Speed{
		set{ speed = value;}
	}

	public virtual bool Play{
		set{ play = value;}
	}
}
