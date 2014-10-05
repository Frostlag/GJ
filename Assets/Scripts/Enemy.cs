using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float speed;
	void Start () {
		rigidbody2D.velocity = Vector3.left * speed;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.sharedMaterial.name == "WallTop") {
			rigidbody2D.velocity = Vector3.left * speed;
		}
		if (other.collider.sharedMaterial.name == "StageEndLeft") {
			Destroy(gameObject);
		}

		if (other.collider.sharedMaterial.name == "EarthTop") {
			Destroy(gameObject);
		}

	}
}
