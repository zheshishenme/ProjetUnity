using UnityEngine;
using System.Collections;

public class effetGomme : MonoBehaviour {

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
			Destroy(col.gameObject.transform.Find("departTrainee").gameObject);
			GameObject go = Instantiate(Resources.Load("departTrainee"),col.gameObject.transform.position, col.gameObject.transform.rotation) as GameObject; 
			go.name = "departTrainee";
			go.transform.parent = col.gameObject.transform;
			lanceAnimation();
		}
	}
	
	void lanceAnimation(){
		GetComponent<MeshRenderer>().enabled=false;
		GetComponent<CapsuleCollider>().enabled = false;
		Destroy(gameObject);
		//GetComponent<ParticleSystem>().Play();
	}
}
