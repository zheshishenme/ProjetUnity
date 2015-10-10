using UnityEngine;
using System.Collections;

public class bonusSablier : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
		/*if(!GetComponent<ParticleSystem>().IsAlive()){
			Destroy(gameObject);
		}*/
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "tete"){
			ControlleurJeu.instance.ajouterTemps();
			lanceAnimation();
		}
	}
	
	void lanceAnimation(){
		Destroy(gameObject);
		GetComponent<MeshRenderer>().enabled=false;
		GetComponent<BoxCollider>().enabled = false;
		//GetComponent<ParticleSystem>().Play();
	}
}
