﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    
    bool isJumping;
	bool isFalling;
	float oldY;
	public int jumpMult;
	public int moveMult;
	public int maxJumpHeight;
	Animator animator;

	void Air(){
	}

	void Earth(){
	}

	void Water(){
	}

	void Fire(){
	}



	void Start () {
        isJumping = true;
		isFalling = true;
		animator = GetComponent<Animator> ();
	}
	
	void Update () {

		handleControls ();
		Vector3 v = rigidbody2D.velocity;
		if (isJumping) {
			if (isFalling){
				v.y = -jumpMult;
			}
			else{
				v.y = jumpMult;
			}

			rigidbody2D.velocity = v;
		}


	}

	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.name == "Floor"){
			isJumping = false;
			isFalling = false;
			animator.SetBool ("jump",false);

		}
		if (other.gameObject.name == "Platform") {
			if (other.collider.GetType() == typeof(UnityEngine.EdgeCollider2D)){
				isJumping = false;
				isFalling = false;
				animator.SetBool ("jump",false);
			}
			else{
				isFalling = true;
			}
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.name == "Platform") {
			if (other.collider.GetType() == typeof(UnityEngine.EdgeCollider2D) &&
			    !isJumping){
				isJumping = true;
				isFalling = true;
				animator.SetBool ("jump",true);
			}
		}
	}

	void handleControls(){
		if (Input.GetButton ("Up") && !isJumping) {
			animator.SetBool ("jump",true);
			isJumping = true;
			oldY = transform.position.y;
		}


		if (isJumping && !isFalling && (!Input.GetButton("Up")) || transform.position.y - oldY > maxJumpHeight){
			isFalling = true;
		}
		Vector3 v = rigidbody2D.velocity;

		v.x = Input.GetAxis ("Horizontal") * moveMult;
		print (v.x);	
		if (v.x > 0f) {
			animator.SetBool ("run", true);
			transform.localScale = new Vector3(5,5,1);
		} else if (v.x < 0f) {
			animator.SetBool ("run", true);
			transform.localScale = new Vector3(-5,5,1);
		} else {
			animator.SetBool("run",false);
		}

		rigidbody2D.velocity = v;



		if (Input.GetButton ("Air")) {
		}
		if (Input.GetButton ("Earth")) {
		}
		if (Input.GetButton ("Water")) {
		}
		if (Input.GetButton ("Fire")) {
		}

	}
}
