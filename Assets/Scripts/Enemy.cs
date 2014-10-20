using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int inithealth;
	int health;
	bool onScreen;

	public float speed;

	void Start () {
		onScreen = false;
		health = inithealth;
	}
	
	void Update () {
		if (!onScreen && GameObject.Find ("RightBound").transform.position.x > transform.position.x) {
			onScreen = true;
			rigidbody2D.velocity = Vector3.left * speed;
		}

		if (onScreen) {
			if (Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Abs(speed)){
				Vector3 v = rigidbody2D.velocity;
				v.x = -speed;
				rigidbody2D.velocity = v;
			}
			rigidbody2D.AddForce(Vector3.left * speed);
		}

		if (health == 0) {
			print ("Killed");
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.sharedMaterial == null)
			return;

		if (other.collider.sharedMaterial.name == "StageEndLeft" ||
		    other.collider.sharedMaterial.name == "DeathPit") {
			Destroy(gameObject);
		}

		if (other.collider.sharedMaterial.name == "EarthTop") {
			Destroy(gameObject);
		}

		if (other.collider.sharedMaterial.name == "Fireball") {
			health--;
		}
	}
}
