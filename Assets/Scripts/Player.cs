using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{    
    bool isJumping;
	bool isFalling;
	int root;

	float oldY;
	
	Animator animator;


	bool airOnCD;
	bool waterOnCD;
	bool fireOnCD;
	bool earthOnCD;		

	public float gravity;
	public float maxFallingVelocity;
	public float jumpMult;
	public float moveMult;

	public float airCDAmount;
	public float fireCDAmount;
	public float waterCDAmount;
	public float earthCDAmount;

	
	public float minJumpHeight;
	public float airDistance;

	public GameObject projectile;
	public GameObject airEffect;
	public GameObject column;
	public GameObject wave;

	void Air()
	{
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

		Vector3 s = temp.transform.localScale;
		s.x = s.x * side;

		temp.transform = s;

		airOnCD = true;
		animator.SetBool ("airing", true);

		Invoke ("AirOffCD", airCDAmount);
		Invoke ("doneAbility", 0.1f);
	}

	void Earth()
	{
		if (isJumping)return;
		if (earthOnCD)return;

		Root (0.5f);
		
		Vector3 v = this.transform.position;
		float side = this.transform.localScale.x / Mathf.Abs(this.transform.localScale.x);
		GameObject newColumn = Instantiate (column) as GameObject;
		
		BoxCollider2D box = GetComponent<BoxCollider2D> ();
		BoxCollider2D otherbox = newColumn.GetComponent<BoxCollider2D> ();	
		Vector3 newPos = v;
		newPos.x = v.x + box.size.x*side*7;
		newPos.y -= otherbox.size.y*5.5f;
		newPos.z = 0;
		newColumn.transform.position = newPos;
		
		Vector3 s = newColumn.transform.localScale;
		s.x = s.x * side;

		newColumn.transform.localScale = s;
		
		earthOnCD = true;
		animator.SetBool ("earthing", true);
		Invoke ("EarthOffCD", earthCDAmount);
		Invoke ("doneAbility", 0.1f);
	}

	void Water()
	{
		if (isJumping)return;
		if (waterOnCD)return;
		
		Root (0.6f);
		
		Vector3 v = this.transform.position;
		float side = this.transform.localScale.x / Mathf.Abs(this.transform.localScale.x);
		GameObject newWave = Instantiate (wave) as GameObject;
		
		BoxCollider2D box = GetComponent<BoxCollider2D> ();
		BoxCollider2D otherbox = newWave.GetComponent<BoxCollider2D> ();	
		Vector3 newPos = v;
		newPos.x = v.x + box.size.x*side*4;

		newPos.z = 0;
		newWave.transform.position = newPos;
		
		Vector3 s = newWave.transform.localScale;
		s.x = s.x * side;
		
		newWave.transform.localScale = s;
		
		waterOnCD = true;
		animator.SetBool ("watering", true);
		Invoke ("WaterOffCD", waterCDAmount);
		Invoke ("doneAbility", 0.1f);
	}

	void Fire()
	{
		if (fireOnCD)return;
		fireOnCD = true;
		Invoke ("FireOffCD", fireCDAmount);	
		Invoke ("FireProjectile", 0.2f);
		animator.SetBool ("firing", true);
		Invoke ("doneAbility", 0.01f);
	}

	void Start ()
	{
        isJumping = true;
		isFalling = true;
		root = 0;
		animator = GetComponent<Animator> ();
		rigidbody2D.gravityScale = gravity;
	}
	
	void Update ()
	{
		handleControls();

		if (isJumping) 
		{
			if (isFalling)
			{
				if (rigidbody2D.velocity.y < -maxFallingVelocity )
				{
					Vector3 v = rigidbody2D.velocity;	
					v.y = -maxFallingVelocity;
					rigidbody2D.velocity = v;
				}
			}
			if (!isFalling &&  rigidbody2D.velocity.y < 0)
			{
				isFalling = true;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "Floor")
		{
			isJumping = false;
			isFalling = false;
			animator.SetBool("jump",false);
			animator.SetBool("fall",false);

		}
		if (other.collider.sharedMaterial.name == "WallTop")
		{
			isJumping = false;
			isFalling = false;
			animator.SetBool ("jump",false);
			animator.SetBool ("fall",false);
		}
		else if(other.collider.sharedMaterial.name == "WallBottom"){
			isFalling = true;
			animator.SetBool ("fall",true);
		}
		else if(other.collider.sharedMaterial.name == "spikes" || 
		        other.collider.sharedMaterial.name == "Enemy" ||
		        other.collider.sharedMaterial.name == "DeathPit")
		{
			Die ();
		}
		
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if (other.collider.sharedMaterial.name == "WallTop")
		{
			if (!isJumping && other.collider.GetType() == typeof(UnityEngine.EdgeCollider2D))
			{
				isJumping = true;
				isFalling = true;
				animator.SetBool ("fall",true);
				animator.SetBool ("jump",true);
			}
		}
	}

	void handleControls()
	{
		Vector3 v = rigidbody2D.velocity;

		if (Input.GetButton ("Up") && !isJumping && root==0)
		{
			rigidbody2D.AddForce(Vector2.up * jumpMult);
			animator.SetBool ("jump",true);
			isJumping = true;
			oldY = transform.position.y;
		}

		if (isJumping && !isFalling && !Input.GetButton("Up") && transform.position.y -oldY > minJumpHeight)
		{
			isFalling = true;
			v.y = 0;
			animator.SetBool ("fall",true);
		}

		if (root == 0)
			v.x = Input.GetAxis ("Horizontal") * moveMult;
		else
			v.x = 0;
			
		if (v.x > 0f)
		{
			animator.SetBool ("run", true);
			transform.localScale = new Vector3(5,5,1);
		}
		else if (v.x < 0f)
		{
			animator.SetBool ("run", true);
			transform.localScale = new Vector3(-5,5,1);
		}
		else
		{
			animator.SetBool("run",false);
		}

		rigidbody2D.velocity = v;

		if (Input.GetButton("Air"))
			Air();

		if (Input.GetButton("Earth"))
			Earth();

		if (Input.GetButton("Water"))
			Water();

		if (Input.GetButton("Fire"))
			Fire();

	}

	void AirOffCD()
	{
		animator.SetBool("airing", false);
		airOnCD = false;
	}

	void WaterOffCD()
	{
		animator.SetBool("watering", false);
		waterOnCD = false;
	}

	void FireOffCD()
	{
		animator.SetBool("firing", false);
		fireOnCD = false;
	}

	void EarthOffCD()
	{
		animator.SetBool("earthing", false);
		earthOnCD = false;
	}

	void Root(float time)
	{
		root += 1;
		Invoke ("Unroot", time);
	}

	void Unroot()
	{
		root -= 1;
	}
	void doneAbility()
	{
		animator.SetBool ("airing", false);
		animator.SetBool ("firing", false);
		animator.SetBool ("earthing", false);
		animator.SetBool ("watering", false);
	}

	void Die()
	{
		Vector3 v = GameObject.Find ("Main Camera").transform.position;
		v.z = 0;
		transform.position = v;
	}

	void FireProjectile()
	{
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
	}
}
