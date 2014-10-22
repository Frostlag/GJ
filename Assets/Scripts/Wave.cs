﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour {
	public float waitTime;
	public float speed;
	public float travelDistance;
	public float explodeForce;

	float oldX;

	List<GameObject> touching;
	// Use this for initialization
	void Start () {
		renderer.enabled = false;
		this.collider2D.enabled = false;
		Invoke ("Appear", waitTime	);
		oldX = transform.position.x;
		touching = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {


		if (Mathf.Abs (oldX - transform.position.x) > travelDistance) {
			Explode ();
		}
	}

	void Explode(){
		float side = this.transform.localScale.x / Mathf.Abs(this.transform.localScale.x);
		foreach (GameObject i in touching)
		{
			if (i == null) continue;
			Vector3 l = i.rigidbody2D.velocity;
			l.x += side * explodeForce;
			i.rigidbody2D.velocity = l;
			l = new Vector3();
		}
		Invoke ("Die", 1f);
	}

	void Die(){
		Destroy (gameObject);
	}

	void Appear(){
		renderer.enabled = true;
		this.collider2D.enabled = true;

		float side = this.transform.localScale.x / Mathf.Abs(this.transform.localScale.x);

		rigidbody2D.velocity = Vector3.right * side * speed;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.sharedMaterial != null && other.collider.sharedMaterial.name != "Enemy") {
			Explode ();
		} else {
			touching.Add (other.gameObject);
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.collider.sharedMaterial != null && other.collider.sharedMaterial.name != "Enemy") {
			touching.Remove (other.gameObject);
		}
	}
}
