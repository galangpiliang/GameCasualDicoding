﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public float minX = 0f; // batas maksimal kiri kamera
	public float maxX = 25.6f; // batas masksimal kanan kamera

	float smoothness = 1; // untuk mengatur agar kamera tidak bergerak terlalu cepat 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x = player.transform.position.x;

		if(pos.x < minX)
			pos.x = minX;
		else if (pos.x > maxX)
		pos.x = maxX;

		transform.position = Vector3.Lerp(transform.position,pos,smoothness*Time.deltaTime);
	}
}
