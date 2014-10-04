using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public GameObject floor;

	void Start () {
	
	}
	

	void Update () {
		Vector3 temp = floor.transform.position;
		temp.x += 0.01f;
		floor.transform.position = temp;
	}
}
