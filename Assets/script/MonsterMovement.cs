using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

	public float movePower = 1f;
	public int score = 0;

	Animator animator;
	Vector3 movement;
	int movementFlag = 0; // 0 : default, 1 : left, 2 : right
	bool isDie = false;

	// <-- initialization
	void Start () {
		animator = gameObject.GetComponentInChildren<Animator>();

		StartCoroutine("ChangeMovement");
	}

	// <-- Coroutine
	IEnumerator ChangeMovement() {
		
		// Random Change Movement
		movementFlag = Random.Range(0, 3);
		
		// Mapping Animation
		if(movementFlag == 0) {
			animator.SetBool("isMoving", false);
		}
		else {
			animator.SetBool("isMoving", true);
		}

		Debug.Log ("Front test" + Time.time);

		yield return new WaitForSeconds(3f);

		Debug.Log ("behind test" + Time.time);

		StartCoroutine("ChangeMovement");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move();
	}

	void Move () {
		Vector3 moveVelocity = Vector3.zero;

		if(movementFlag == 1) {
			moveVelocity = Vector3.left;
			transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		}

		else if(movementFlag == 2) {
			moveVelocity = Vector3.right;
			transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
		}

		if(transform.position.x > -0.98) {
			moveVelocity = Vector3.left;
			transform.position += moveVelocity * movePower * Time.deltaTime;
		}	
		else if(transform.position.x < -10.48) {	
			moveVelocity = Vector3.right;
			transform.position += moveVelocity * movePower * Time.deltaTime;
		}
		else {		
			transform.position += moveVelocity * movePower * Time.deltaTime;
		}
			
	}

	public void die() {
		score = 1000;

		// stop move
		StopCoroutine("ChangeMovement");
		isDie = true;

		// flip y axis
		SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
		renderer.flipY = true;

		// Falling
		BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
		collider.enabled = false;

		// Die and Bouncing
		Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
		Vector2 dieVelocity = new Vector2(0, 10f);
		rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

		// Remove Monster
		Destroy(gameObject, 5f);

	}
}
