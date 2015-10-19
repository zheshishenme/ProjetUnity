using UnityEngine;
using System.Collections;

public class etoilePoint : MonoBehaviour {

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
			//GameObject player = col.gameObject.transform.parent.gameObject;

			ControlleurJoueur.instance.score ++ ;
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

