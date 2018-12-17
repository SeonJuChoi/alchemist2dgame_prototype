using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class character_move_jump : MonoBehaviour {

    public float movePower = 1f;
    public float jumpPower = 1f;
    public int maxHP = 3;

    Rigidbody2D rigid;
	Animator animator;

    Vector3 movement;

    bool isDie = false;
    bool isJumping = false;
    int hp = 3;

	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponentInChildren<Animator>();

        hp = maxHP;
	}
	
	// Update is called once per frame
	void Update () {

         
        // check HP
        if(hp == 0) {
            if(!isDie) {
                Die();
            }
        }     

		if (Input.GetAxisRaw("Horizontal") == 0) {
			animator.SetBool("isMoving", false);
		}
		else if(Input.GetAxisRaw("Horizontal") < 0) {
			animator.SetInteger("Direction", -1);
			animator.SetBool("isMoving", true);
		}
		else if(Input.GetAxisRaw ("Horizontal") > 0) {
			animator.SetInteger("Direction", 1);
			animator.SetBool("isMoving", true);
		}
        if(Input.GetButtonDown("Jump") && !animator.GetBool("isJumping")) {
            isJumping = true;
			animator.SetBool("isJumping", true); // Jump Flag
			animator.SetTrigger("doJumping"); // Jump Animation
        }
	}

    // Physics engine Updates
    void FixedUpdate () {

         // check HP
         if(hp == 0) {
             if(!isDie) {
                 Die();
             }
         } 

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

    void Die() {

        // Die Flag Setting
        isDie = true;

        rigid.velocity = Vector2.zero;

        // flip y axis
		SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
		renderer.flipY = true;

        BoxCollider2D[] colls = gameObject.GetComponents<BoxCollider2D>();
        colls[0].enabled = false;
        colls[1].enabled = false;

        Vector2 dieVelocity = new Vector2(0, 10f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

        Invoke("RestartStage", 2f);
        
    }

    void RestartStage() {
        GameManager.RestartStage();
    }

	// Attach Event
    void OnTriggerEnter2D (Collider2D other) {
		if((other.gameObject.layer == 8 || other.gameObject.layer == 9)){
			animator.SetBool("isJumping", false);
		}

		if(other.gameObject.layer == 9 && rigid.velocity.y < 0){
            
            Block_obj_status block = other.gameObject.GetComponentInChildren<Block_obj_status>(true);
            Debug.Log(block.type);

            switch(block.type) {
                case "Up" : {
                    Vector2 upVelocity = new Vector2(0, block.value);
                    rigid.AddForce(upVelocity, ForceMode2D.Impulse);
                    break;
                }
            }
        }

        Debug.Log(other.gameObject.tag);
        Debug.Log("y - " + rigid.velocity.y);

        if(other.gameObject.tag == "mob" && !other.isTrigger && rigid.velocity.y < -5) {
            
            
            // bouncing
            Vector2 killVelocity = new Vector2(0, 10f);
            rigid.AddForce(killVelocity, ForceMode2D.Impulse);

            // kill monster
            MonsterMovement monster = other.gameObject.GetComponent<MonsterMovement>();
            monster.die();

            // get Score
            ScoreClass.setScore(monster.score);

            Debug.Log(monster.score);

            ScoreClass.addScore();

        }
        else if(other.gameObject.tag == "mob" && !other.isTrigger && !(rigid.velocity.y < -5)) {
            
            Vector2 attackedVelocity = Vector2.zero;

            if(other.gameObject.transform.position.x > transform.position.x){
                attackedVelocity = new Vector2(-2f, 3f);
            }
            else {
                attackedVelocity = new Vector2(2f, 3f);
            }

            rigid.AddForce(attackedVelocity, ForceMode2D.Impulse);

            hp--;
            Debug.Log(hp);
        }
        
        if(other.gameObject.tag == "gameover") {
            hp = 0;
        }
    
        if(other.gameObject.tag == "coin") {
            // get Score
            Block_obj_status coin = other.gameObject.GetComponent<Block_obj_status>();
            ScoreClass.setScore((int)coin.value);
     
            // Remove Coin
            Destroy(other.gameObject, 0f);

            ScoreClass.addScore();
        } 

        if(other.gameObject.tag == "goal") {
            GameManager.EndGame();
        } 
	}

	// Detach Event 
	void OnTriggerExit2D (Collider2D other) {
		Debug.Log("Detach" + other.gameObject.layer);
	}

    
}
