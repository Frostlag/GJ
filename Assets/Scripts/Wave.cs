using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {
	public float waitTime;
	// Use this for initialization
	void Start () {
		renderer.enabled = false;
		this.collider2D.enabled = false;
		Invoke ("Appear", waitTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Appear(){
		renderer.enabled = true;
		this.collider2D.enabled = true;
	}
}
