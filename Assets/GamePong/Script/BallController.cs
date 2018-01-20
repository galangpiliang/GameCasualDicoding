using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	public int force;
	int scoreP1;
	int scoreP2;

	AudioSource audio;
	public AudioClip hitSound;

	GameObject scoreP1UI;
	GameObject scoreP2UI;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(2,1).normalized*force);
		//inisialisasi score
		scoreP1 = 0;
		scoreP2 = 0;

		//mencari GameObject dengan nama tertentu
		scoreP1UI = GameObject.Find("Score P1");
		scoreP2UI = GameObject.Find("Score P2");

		//mencari GameObject audiosource
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll){
		//membunyikan audio
		audio.PlayOneShot(hitSound);

		//apabila bola bertabrakan dengan pemukul
		if(coll.gameObject.name == "Pemukul P1" || coll.gameObject.name == "Pemukul P2"){
			//mencatat arah pergerakan bola
			Vector2 direction = new Vector2 (GetComponent<Rigidbody2D>().velocity.x,
			GetComponent<Rigidbody2D>().velocity.y).normalized;
			//menambahkan force pada bola
			GetComponent<Rigidbody2D>().AddForce(direction*force);
		}
		//apabila bola bertabrakan dengan tepi kiri
		else if(coll.gameObject.name == "Tepi Kiri"){
			scoreP2+=1;
			ResetBall();
			//menggerakkan bola
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-2,1).normalized*force);
		}
		//apabila bola bertabrakan dengan tepi kanan
		else if(coll.gameObject.name == "Tepi Kanan"){
			scoreP1 += 1;
			ResetBall();
			//menggerakkan bola
			GetComponent<Rigidbody2D>().AddForce(new Vector2(2,1).normalized*force);
		}

		//Debug.Log("Score P1: "+scoreP1+" Score P2: "+scoreP2);
		//output score pada layar
		scoreP1UI.GetComponent<Text>().text = "" + scoreP1;
		scoreP2UI.GetComponent<Text>().text = "" + scoreP2;
		
	}

	//memindahkan bola ke titik awal, set kecepatan bola menjadi 0
	void ResetBall(){
		transform.localPosition = new Vector2 (0,0);
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
	}
}
