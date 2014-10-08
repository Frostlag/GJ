using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject mc;
	public GameObject enemy;
	public GameObject floor;
	public GameObject platform;
	public GameObject[] levelPieces;

	public float chanceofplatform;

	float lastPieceX;
	float xPerPiece = 22;

	void Start () {
		lastPieceX = mc.transform.position.x-xPerPiece/10*9;
	}
	

	void Update () {
		moveFloor ();
		moveCamera ();
		generatePlatform ();
	
	}

	void generatePlatform (){
		Vector3 v = mc.transform.position;

		if (v.x - lastPieceX > xPerPiece){
			lastPieceX = v.x;
			int roll = Random.Range (0, levelPieces.Length);
			v.z = 0;
			v.y = 0;
			v.x += xPerPiece;
			GameObject temp = Instantiate (levelPieces[roll],v,Quaternion.identity) as GameObject;

		}
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

	void GameOver(){
	
	}
	              
}
