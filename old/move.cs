using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

	public float speed;
	
	void Update () 
	{		
		UpdateKeyboard ();
	}
	
	void UpdateKeyboard()
	{
		if (Input.GetKey("up"))
			avancer();
		if (Input.GetKey("down"))
			reculer();
		if (Input.GetKey("left"))
			gauche();
		if (Input.GetKey("right"))
			droite();
	}
	
	public void Move(Vector3 v)
	{
		transform.Translate(
			v.x,
			v.y,
			v.z,
			Space.World
			);
	}

	public void avancer(){
		gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*speed);
	}

	public void reculer(){
		gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward*speed);
	}

	public void droite(){
		gameObject.GetComponent<Rigidbody>().AddForce(transform.right*speed);
	}

	public void gauche(){
		gameObject.GetComponent<Rigidbody>().AddForce(-transform.right*speed);
	}

}
