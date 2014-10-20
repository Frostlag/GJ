using UnityEngine;
using System.Collections;

public class air : MonoBehaviour {
	public float lifetime;
	public float speed;

	Vector3 v;
	// Use this for initialization
	void Start () {
		Invoke ("Die",lifetime);
		Vector3 s = transform.localScale;
		float side = s.x / Mathf.Abs (s.x);
		v.x = speed * side;
		v.y = 0;
		v.z = 0;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += v;
	}

	void Die(){
		Destroy (gameObject);
	}
}
