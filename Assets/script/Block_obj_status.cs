using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_obj_status : MonoBehaviour {

	public string type;
	public float value = 0;

	Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();		
	}

	// Attach Event
    void OnTriggerEnter2D (Collider2D other) {
		 if(other.gameObject.tag == "coin") {
			// Remove Coin
            Destroy(other.gameObject, 0f);
        } 
	}

}
