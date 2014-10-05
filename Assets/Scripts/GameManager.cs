using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject mc;
	public GameObject floor;
	public GameObject platform;

	public float chanceofplatform;

	void Start () {
		Vector3 v = mc.transform.position;
		v.z = 0;
		GameObject temp = Instantiate (platform,v,Quaternion.identity) as GameObject;
		temp.tag = "Platform";
	}
	

	void Update () {
		moveFloor ();
		moveCamera ();
		generatePlatform ();
	}

	void generatePlatform (){
		float roll = Random.Range (0f, 1f);
	}

	void moveFloor(){
		Vector3 temp = floor.transform.position;
		temp.x += 0.01f;
		floor.transform.position = temp;
	}

	void moveCamera(){
		Vector3 temp = mc.transform.position;
		temp.x += 0.01f;
		mc.transform.position = temp;
	}
}
