using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    
    bool isJumping;
	bool isFalling;
	bool root;
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
	public float minJumpHeight;
	public float airDistance;

	public GameObject projectile;
	public GameObject airEffect;
	public GameObject column;

	void Air(){
		if (airOnCD)return;
		Vector3 v = this.transform.position;
		float side = this.transform.localScale.x / Mathf.Abs(this.transform.localScale.x);
		BoxCollider2D box = GetComponent<BoxCollider2D> ();

		v.x += side * airDistance;
		v.y += box.size.y / 2;

		this.transform.position = v;

		v.x -= side * airDistance / 2;

		GameObject temp = Instantiate (airEffect) as GameObject;
		temp.transform.position = v;

		airOnCD = true;
		animator.SetBool ("airing", true);

		Invoke ("AirOffCD", airCDAmount);
		Invoke ("doneAbility", 0.1f);



	}

	void Earth(){
		if (isJumping)return;
		if (earthOnCD)return;

		Root (1f);
		
		Vector3 v = this.transform.position;
		float side = this.transform.localScale.x / Mathf.Abs(this.transform.localScale.x);
		GameObject newColumn = Instantiate (column) as GameObject;
		
		BoxCollider2D box = GetComponent<BoxCollider2D> ();
		BoxCollider2D otherbox = newColumn.GetComponent<BoxCollider2D> ();	
		Vector3 newPos = v;
		newPos.x = v.x + box.size.x*side*8;
		newPos.y -= otherbox.size.y*2.5f;
		print (newPos.y);
		newPos.z = 0;
		newColumn.transform.position = newPos;
		
		Vector3 s = newColumn.transform.localScale;
		s.x = s.x * side;

		newColumn.transform.localScale = s;
		
		earthOnCD = true;
		animator.SetBool ("earthing", true);
		Invoke ("EarthOffCD", fireCDAmount);
		Invoke ("doneAbility", 0.1f);
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
		animator.SetBool ("firing", true);
		Invoke ("FireOffCD", fireCDAmount);
		Invoke ("doneAbility", 0.01f);

	}



	void Start () {
        isJumping = true;
		isFalling = true;
		root = false;
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
		if (Input.GetButton ("Up") && !isJumping && !root) {
			animator.SetBool ("jump",true);
			isJumping = true;
			oldY = transform.position.y;
		}


		if (isJumping && !isFalling && transform.position.y - oldY > minJumpHeight && (!Input.GetButton("Up")) || transform.position.y - oldY > maxJumpHeight){
			isFalling = true;
			animator.SetBool ("fall",true);
		}
		Vector3 v = rigidbody2D.velocity;

		if (!root)
						v.x = Input.GetAxis ("Horizontal") * moveMult;
				else
						v.x = 0;
			
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
		animator.SetBool ("airing", false);
		airOnCD = false;
	}
	void WaterOffCD(){
		waterOnCD = false;
	}
	void FireOffCD(){
		animator.SetBool ("firing", false);
		fireOnCD = false;
	}
	void EarthOffCD(){
		animator.SetBool ("earthing", false);
		earthOnCD = false;
	}

	void Root(float time){
		root = true;
		Invoke ("Unroot", time);
	}

	void Unroot(){
		root = false;
	}
	void doneAbility(){

		animator.SetBool ("airing", false);
		animator.SetBool ("firing", false);
		animator.SetBool ("earthing", false);
		animator.SetBool ("watering", false);
	}
}
