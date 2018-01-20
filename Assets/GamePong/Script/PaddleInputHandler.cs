using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleInputHandler : MonoBehaviour {

	public int moveSpeed = 5;//kecepatan pergerakkan
	public string keyControl;//mengatur input keyboard

	int currentState;//mencatat state saat ini

	const int NORMAL = 0;
	const int STUCK_ATAS = 1;
	const int STUCK_BAWAH = 2;

	// Use this for initialization
	void Start () {
		currentState = NORMAL;
	}
	
	// Update is called once per frame
	void Update () {
		// menghitung perpindahan posisi y berdasarkan input keyboard dan kecepatan pergerakan
		float yChange = Input.GetAxis(keyControl)*moveSpeed;

		//pengaturan berdasarkan state
		switch (currentState){
			case STUCK_ATAS:
				if(yChange <= 0){
					currentState = NORMAL;
				}
				break;
			case STUCK_BAWAH:
				if (yChange >= 0){
					currentState = NORMAL;
				}
				break;
			default:
			//memindahkan pemukul
			transform.position = new Vector3(transform.position.x,transform.position.y+yChange,transform.position.z);
			break;
		}

				
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.name == "Tepi Atas"){
			currentState = STUCK_ATAS;
		}else if(coll.gameObject.name == "Tepi Bawah"){
			currentState = STUCK_BAWAH;
		}
	}
}
