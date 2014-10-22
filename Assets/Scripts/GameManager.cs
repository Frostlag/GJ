using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject camera;
	public GameObject enemy;
	public GameObject floor;
	public GameObject[] levelPieces;

	public float screenSpeed = 0.03f;

	float lastPieceX;
	float xPerPiece = 22;

	void start()
	{
		lastPieceX = camera.transform.position.x - xPerPiece / 10 * 9;
	}

	void update()
	{
		moveScreen();
		generatePlatform ();
	}

	void generatePlatform()
	{
		Vector3 v = camera.transform.position;

		if (v.x - lastPieceX > xPerPiece){
			lastPieceX = v.x;
			int roll = Random.Range (0, levelPieces.Length);
			v.z = 0;
			v.y = 0;
			v.x += xPerPiece;
			GameObject temp = Instantiate (levelPieces[roll],v,Quaternion.identity) as GameObject;

		}
	}
	
	void moveScreen()
	{
		Vector3 floorPosition = floor.transform.position;
		floorPosition.x += screenSpeed;
		floor.transform.position = floorPosition;
		
		Vector3 cameraPosition = camera.transform.position;
		cameraPosition.x += screenSpeed;
		camera.transform.position = cameraPosition;
	}

	void gameOver()
	{
	
	}
	              
}
