using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_movement : MonoBehaviour {

    public float movePower = 1f;
    public float jumpPower = 1f;

    Rigidbody2D rigid;
    Vector3 movement;

    bool isJumping = false;

	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Jump")) {
            isJumping = true;
        }
	}

    // Physics engine Updates
    void FixedUpdate () {
            Move();
            Jump();
    }

    /* <-- Movement Function */
    void Move() {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw ("Horizontal") < 0) {
            moveVelocity = Vector3.left;
        }
        else if(Input.GetAxisRaw("Horizontal") > 0) {
            moveVelocity = Vector3.right;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    /* <-- Jump Function */
    void Jump() {
        if (!isJumping)
            return;
        
        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
        
    }
}
