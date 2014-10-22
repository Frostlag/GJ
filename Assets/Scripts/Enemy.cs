using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int inithealth;
	public float speed;
	int health;
	bool onScreen;
	bool withPlayer;

	GameObject player;

	Animator animator;

	// Override
	void Start () {
		onScreen = false;
		withPlayer = false;
		health = inithealth;
		animator = GetComponent<Animator>();
	}

	// Override
	void Update () {
		if (!onScreen && GameObject.Find ("RightBound").transform.position.x > transform.position.x) {
			onScreen = true;
			rigidbody2D.velocity = Vector3.left * speed;
		}

		if (onScreen) {
			rigidbody2D.AddForce(Vector3.left * speed);
		}

		if (health == 0) {
			Destroy(gameObject);
		}
	}

	// Override
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

		if (other.collider.sharedMaterial.name == "Player") {
			withPlayer = true;
			StartCoroutine(doneSwing());
			player = other.gameObject;
			animator.SetBool("swing",true);
		}


	}

	// Override
	void OnCollisionExit2D(Collision2D other){
		if (other.collider.sharedMaterial.name == "Player") {
			withPlayer = false;
		}
	}

	private IEnumerator doneSwing(){
		yield return new WaitForSeconds (0.5f);

		if (withPlayer){
			player.SendMessage("die");

		}
		animator.SetBool("swing",false);
	}
}
