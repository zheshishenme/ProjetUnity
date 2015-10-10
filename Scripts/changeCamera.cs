using UnityEngine;
using System.Collections;

public class changeCamera : MonoBehaviour {

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
			col.gameObject.transform.parent.gameObject.GetComponent<ControlleurJoueur>().changeCamera();
			lanceAnimation();
		}
	}
	
	void lanceAnimation(){
		GetComponent<MeshRenderer>().enabled=false;
		GetComponent<SphereCollider>().enabled = false;
		//GetComponent<ParticleSystem>().Play();
		Destroy(gameObject);
	}
}
