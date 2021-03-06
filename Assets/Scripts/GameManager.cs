﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject enemy;
	public GameObject floor;
	public GameObject[] levelPieces;

	public float screenSpeed = 0.03f;

	float lastPieceX;
	float xPerPiece = 22;

	List<GameObject> onStagePieces;

	// Override
	void Start() 
	{
		lastPieceX = mainCamera.transform.position.x - xPerPiece / 10 * 9;
		onStagePieces = new List<GameObject> ();
	}
	// Override
	void Update()
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
			int roll = Random.Range (0, levelPieces.Length);
			v.z = 0;
			v.y = 0;
			v.x += xPerPiece;

			onStagePieces.Add(Instantiate (levelPieces[roll],v,Quaternion.identity) as GameObject);
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
