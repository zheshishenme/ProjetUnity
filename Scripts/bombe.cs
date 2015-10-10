using UnityEngine;
using System.Collections;

public class bombe : MonoBehaviour {

	public GameObject gameobjectImmunised;
	float lifeTimer=-1;
	float timeOfLife = 6;
	bool canBeDestroy = false;

	void Start () {
		lifeTimer = 0;
	}

	// Update is called once per frame
	void Update () {
		if(lifeTimer>=0){
			lifeTimer+=Time.deltaTime;
			if(lifeTimer>=timeOfLife){
				lanceAnimation();
			}
			if(canBeDestroy){
				if(!GetComponent<ParticleSystem>().IsAlive()){
					Destroy(gameObject);
				}
			}
		}	
	}
	
	void OnCollisionEnter(Collision col){ // dès qu'une collision est détectée
		ControlleurJoueur joueur  = col.gameObject.transform.parent.GetComponent<ControlleurJoueur>();

		if(gameobjectImmunised==null){
			if(col.gameObject.tag=="tete"){ // si le collider détecté est taggé "trait" ou "mur"
				lanceAnimation();
				if(joueur.nbVie>1){
					joueur.enlever1Vie();
				}
				else{
					joueur.detruire();
				}
			}
		}
	}
	
	void OnParticleCollision(GameObject go){
		if(go.tag != "mur" && go.tag != "trait"){
			if(go.tag =="tete"){
				ControlleurJoueur joueur  = go.transform.parent.GetComponent<ControlleurJoueur>();

				if(joueur.nbVie>1){
					joueur.enlever1Vie();
				}
				else{
					joueur.detruire();
				}
			}
			else{//detruire les bonnus
				Destroy(go);
			}
		}
	}

	void lanceAnimation(){
		canBeDestroy = false;
		GetComponent<MeshRenderer>().enabled=false;
		GetComponent<SphereCollider>().enabled = false;
		GetComponent<ParticleSystem>().Play();
	}
}
