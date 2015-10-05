using UnityEngine;
using System.Collections;

public class bonusExplosion : MonoBehaviour {
	
	public GameObject gameobjectImmunised;
	float lifeTimer=-1;
	float timeOfLife = 3;

	// Update is called once per frame
	void Update () {
		if(lifeTimer>=0){
			lifeTimer+=Time.deltaTime;
			if(lifeTimer>=timeOfLife){
				Destroy(gameObject);
			}
		}
	}
	
	void OnCollisionEnter(Collision col){ // dès qu'une collision est détectée
		if(gameobjectImmunised==null){
			if(col.gameObject.tag=="tete"){ // si le collider détecté est taggé "trait" ou "mur"
				lifeTimer = 0;
				lanceAnimation();
				gameobjectImmunised = col.gameObject.transform.parent.gameObject;
			}
		}
	}
	
	void OnParticleCollision(GameObject go){
		if(go.tag =="tete"){
			if(go.transform.parent.gameObject != gameobjectImmunised){
				go.transform.parent.GetComponent<ControlleurJoueur>().detruire();
				//Destroy(gameObject);
			}
		}
	}

	void lanceAnimation(){
		GetComponent<MeshRenderer>().enabled=false;
		GetComponent<SphereCollider>().enabled = false;
		GetComponent<ParticleSystem>().Play();
	}
}
