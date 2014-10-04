using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    
    bool isJumping;
	public int jumpMult = 200;
	public int moveMult = 25;
	

	void Start () {
        isJumping = false;
	}
	
	void Update () {
		handleControls ();
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.name == "Floor") {

			isJumping = false;

		}
	}

	void handleControls(){
		if (Input.GetButton ("Up") && !isJumping) {
			isJumping = true;
			rigidbody2D.AddForce(Vector3.up*jumpMult);
		}
		rigidbody2D.AddForce (Vector3.right * Input.GetAxis ("Horizontal") * moveMult);
	}
}
