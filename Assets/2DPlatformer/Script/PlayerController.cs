using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Animator anim; //animator dari player
	Rigidbody2D rigid; //rigidbody 2d dari player

	public bool isGrounded = false; //untuk menyimpan state apakah karakter berada di ground
	public bool isFacingRight = true; //untuk mengetahui arah hadap dari player
	public float jumpForce = 200f; //besar gaya untuk mengangkat player ke atas
	public float walkForce = 15f; //besar gaya untuk mendorong karakter ke samping
	public float maxSpeed = 1.5f; //kecepatan maksimum dari karakter utama

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		InputHandler();
		anim.SetInteger("Speed",(int)rigid.velocity.x);
	}

	void InputHandler(){
		if(Input.GetKey(KeyCode.LeftArrow)){
			MoveLeft();
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			MoveRight();
		}
		if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded){
			Jump();
		}
	}

	void MoveLeft(){
		if (rigid.velocity.x * -1 < maxSpeed)
			rigid.AddForce(Vector2.left * walkForce);

		//membalik arah karakter apabila menghadap ke arah yang berlawanan dari seharusnya
		if(isFacingRight){
			Flip();
		}
	}

	void MoveRight(){
		if (rigid.velocity.x * 1 < maxSpeed)
			rigid.AddForce(Vector2.right * walkForce);

		//membalik arah karakter apabila menghadap ke arah yang berlawanan dari seharusnya
		if(!isFacingRight){
			Flip();
		}
	} 

	void Jump(){
		rigid.AddForce(Vector2.up * jumpForce);
	}

	void Flip(){
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		isFacingRight = !isFacingRight;
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.CompareTag("Ground")){
			anim.SetBool("IsGrounded",true);
			isGrounded = true;
		}
	}

	//digunakan untuk mengecek apakah Player masih diatas tanah atau tidak
	void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.CompareTag("Ground")){
			anim.SetBool("IsGrounded",true);
			isGrounded = true;
		}
	}

	//digunakan untuk memberi tahu Player bahwa sudah tidak diatas tanah
	void OnCollisionExit2D(Collision2D col){
		if(col.gameObject.CompareTag("Ground")){
			anim.SetBool ("IsGrounded",false);
			isGrounded = false;
		}
	}
}
