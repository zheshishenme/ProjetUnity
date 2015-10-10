using UnityEngine;
using System.Collections;

public class EffetSpeedUp : MonoBehaviour {

	float timer = -10f;
	float speedToUp = 2;
	GameObject playerAffected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(timer != -10f){
			timer-=Time.deltaTime;
			
			if(timer<=0){
				if(playerAffected!=null){
					playerAffected.GetComponent<mouvement>().ajouterSpeed(-speedToUp);
					timer = -10f;
					Destroy (gameObject);
				}
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "tete"){
			playerAffected = col.gameObject;
			timer = 5;
			playerAffected.GetComponent<mouvement>().ajouterSpeed(speedToUp);
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			gameObject.GetComponent<Collider>().enabled = false;
		}
	}
}
