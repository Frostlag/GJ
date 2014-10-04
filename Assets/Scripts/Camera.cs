using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public GameObject followed;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.position;
		temp.x += 0.01f;
		this.transform.position = temp;
	}
}
