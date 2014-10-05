using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    
    bool isJumping;
	bool isFalling;
	float oldY;
	Animator animator;

	bool airOnCD;
	bool waterOnCD;
	bool fireOnCD;
	bool earthOnCD;

	public float airCDAmount;
	public float fireCDAmount;
	public float waterCDAmount;
	public float earthCDAmount;

	public float jumpMult;
	public float moveMult;
	public float maxJumpHeight;
	public float airDistance;

	public GameObject projectile;

	void Air(){
		if (airOnCD)return;
		Vector3 v = this.transform.position;
		float side = this.transform.localScale.x / Mathf.Abs(this.transform.localScale.x);

		v.x += side * airDistance;

		this.transform.position = v;

		airOnCD = true;
		Invoke ("AirOffCD", airCDAmount);

	}

	void Earth(){
	}

	void Water(){
	}

	void Fire(){
		if (fireOnCD)return;

		Vector3 v = this.transform.position;
		float side = this.transform.localScale.x / Mathf.Abs(this.transform.localScale.x);
		GameObject newProjectile = Instantiate (projectile) as GameObject;

		BoxCollider2D box = GetComponent<BoxCollider2D> ();
		Vector3 newPos;
		newPos.x = v.x + box.size.x*side*4;
		newPos.y = v.y;
		newPos.z = 0;
		newProjectile.transform.position = newPos;

		Vector3 s = newProjectile.transform.localScale;
		s.x = s.x * side;
		newProjectile.transform.localScale = s;
				
		fireOnCD = true;
		Invoke ("FireOffCD", fireCDAmount);
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
			animator.SetBool ("fall",false);

		}


			if (other.collider.sharedMaterial.name == "WallTop"){
				isJumping = false;
				isFalling = false;
				animator.SetBool ("jump",false);
				animator.SetBool ("fall",false);
			}
			else if(other.collider.sharedMaterial.name == "WallBottom"){
				isFalling = true;
				animator.SetBool ("fall",true);
			}
		
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.collider.sharedMaterial.name == "WallTop") {
			if (other.collider.GetType() == typeof(UnityEngine.EdgeCollider2D) &&
			    !isJumping){
				isJumping = true;
				isFalling = true;
				animator.SetBool ("fall",true);
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
			animator.SetBool ("fall",true);
		}
		Vector3 v = rigidbody2D.velocity;

		v.x = Input.GetAxis ("Horizontal") * moveMult;
			
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



		if (Input.GetButton ("Air")) Air ();

		if (Input.GetButton ("Earth")) Earth ();

		if (Input.GetButton ("Water")) Water();

		if (Input.GetButton ("Fire"))Fire ();

	}

	void AirOffCD(){
		airOnCD = false;
	}
	void WaterOffCD(){
		waterOnCD = false;
	}
	void FireOffCD(){
		fireOnCD = false;
	}
	void EarthOffCD(){
		earthOnCD = false;
	}
}
