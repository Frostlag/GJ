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

	void Start () {
		onScreen = false;
		withPlayer = false;
		health = inithealth;
		animator = GetComponent<Animator>();
	}
	
	void Update () {
		if (!onScreen && GameObject.Find ("RightBound").transform.position.x > transform.position.x) {
			onScreen = true;
			rigidbody2D.velocity = Vector3.left * speed;
		}

		if (onScreen) {
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

		if (other.collider.sharedMaterial.name == "Player") {
			withPlayer = true;
			StartCoroutine(DoneSwing());
			player = other.gameObject;
			animator.SetBool("swing",true);
		}


	}

	void OnCollisionExit2D(Collision2D other){
		if (other.collider.sharedMaterial.name == "Player") {
			withPlayer = false;
		}
	}

	private IEnumerator DoneSwing(){
		yield return new WaitForSeconds (0.5f);

		if (withPlayer){
			player.SendMessage("Die");

		}
		animator.SetBool("swing",false);
	}
}
