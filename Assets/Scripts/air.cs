using UnityEngine;
using System.Collections;

public class air : MonoBehaviour {
	public float lifetime;
	public float speed;

	Vector3 v;

	//Override
	void Start () {
		Invoke ("die",lifetime);
		Vector3 s = transform.localScale;
		float side = s.x / Mathf.Abs (s.x);
		v.x = speed * side;
		v.y = 0;
		v.z = 0;
	}
	
	//Override
	void Update () {
		this.transform.position += v;
	}

	void die(){
		Destroy (gameObject);
	}
}
