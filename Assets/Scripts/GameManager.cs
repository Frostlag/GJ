using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject enemy;
	public GameObject floor;
	public GameObject[] levelPieces;

	public float screenSpeed = 0.03f;
	public float chanceofplatform;

	float lastPieceX;
	float xPerPiece = 22;

	void Start() // Override
	{
		lastPieceX = mainCamera.transform.position.x - xPerPiece / 10 * 9;
	}

	void Update() // Override
	{
		moveScreen();
		generatePlatform();
	}

	void generatePlatform()
	{
		Vector3 v = mainCamera.transform.position;

		if (v.x - lastPieceX > xPerPiece)
		{
			lastPieceX = v.x;
			v.z = 0;
			v.y = 0;
			v.x += xPerPiece;
		}
	}
	
	void moveScreen()
	{
		Vector3 floorPosition = floor.transform.position;
		floorPosition.x += screenSpeed;
		floor.transform.position = floorPosition;
		
		Vector3 mainCameraPosition = mainCamera.transform.position;
		mainCameraPosition.x += screenSpeed;
		mainCamera.transform.position = mainCameraPosition;
	}

	void gameOver()
	{
	
	}
	              
}
