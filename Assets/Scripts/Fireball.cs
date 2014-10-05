using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
		Vector3 s = transform.localScale;
		float side = s.x / Mathf.Abs (s.x);
		
		Vector3 v;
		v.x = speed * side;
		v.y = 0;
		v.z = 0;
		rigidbody2D.velocity = v;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other){
			Destroy(gameObject);
	}
}
