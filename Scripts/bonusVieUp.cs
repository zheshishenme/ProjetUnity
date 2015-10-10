using UnityEngine;
using System.Collections;

public class bonusVieUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*if(!GetComponent<ParticleSystem>().IsAlive()){
			Destroy(gameObject);
		}*/
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "tete"){
			// en mode solo : ControlleurJoueur.Instance.ajouter1Vie();
			GameObject playerAffected = col.gameObject.transform.parent.gameObject;
			playerAffected.GetComponent<ControlleurJoueur>().ajouter1Vie();
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