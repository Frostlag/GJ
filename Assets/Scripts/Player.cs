using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{    
    public bool isJumping;
	public bool isFalling;
	int root;

	float oldY;
	
	Animator animator;

	public float gravity;
	public float maxAirVelocity;
	public float jumpMult;
	public float moveMult;


	public float minJumpHeight;



	//Override
	void Start ()
	{
        isJumping = true;
		isFalling = true;
		root = 0;
		animator = GetComponent<Animator> ();
		rigidbody2D.gravityScale = gravity;
		gameObject.AddComponent (typeof(AirAbility));
		gameObject.AddComponent (typeof(EarthAbility));
		gameObject.AddComponent (typeof(WaterAbility));
		gameObject.AddComponent (typeof(FireAbility));

	}

	//Override
	void Update ()
	{
		handleControls();

		if (isJumping) 
		{
			if (isFalling)
			{
				if (rigidbody2D.velocity.y < -maxAirVelocity )
				{
					Vector3 v = rigidbody2D.velocity;	
					v.y = -maxAirVelocity;
					rigidbody2D.velocity = v;
				}
			}
			if (!isFalling &&  rigidbody2D.velocity.y < 0)
			{
				isFalling = true;
			}

			if (rigidbody2D.velocity.y > maxAirVelocity)
			{
				Vector3 v = rigidbody2D.velocity;	
				v.y = maxAirVelocity;
				rigidbody2D.velocity = v;
			}
		}
	}

	//Override
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
		        //other.collider.sharedMaterial.name == "Enemy" ||
		        other.collider.sharedMaterial.name == "DeathPit")
		{
			die ();
		}
		
	}

	//Override
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

		if (Input.GetButton ("Air"))
			GetComponent<AirAbility>().cast ();

		if (Input.GetButton("Earth") && !isJumping)
			GetComponent<EarthAbility>().cast ();

		if (Input.GetButton("Water") && !isJumping)
			GetComponent<WaterAbility>().cast ();

		if (Input.GetButton("Fire"))
			GetComponent<FireAbility>().cast ();

	}

	
	public void rootMove(float time)
	{
		root += 1;
		Invoke ("unroot", time);
	}

	void unroot()
	{
		if (root > 0) root -= 1;
	}

	void die()
	{
		Vector3 v = GameObject.Find ("Main Camera").transform.position;
		v.z = 0;
		isJumping = true;
		isFalling = true;
		transform.position = v;
	}
}
