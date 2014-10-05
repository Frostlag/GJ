using UnityEngine;
using System.Collections;

public class air : MonoBehaviour {
	public float lifetime;
	// Use this for initialization
	void Start () {
		Invoke ("Die",lifetime	);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Die(){
		Destroy (gameObject);
	}
}
