using UnityEngine;
using System.Collections;

public class bonusExplosion : MonoBehaviour {
	
	public GameObject gameobjectImmunised;
	bool canBeDestroy = false;

	// Update is called once per frame
	void Update () {
		if(canBeDestroy){
			if(!GetComponent<ParticleSystem>().IsAlive()){
				Destroy(gameObject);
			}
		}
	}
	
	void OnCollisionEnter(Collision col){ // dès qu'une collision est détectée
		if(gameobjectImmunised==null){
			if(col.gameObject.tag=="tete"){ // si le collider détecté est taggé "trait" ou "mur"
				lanceAnimation();
				gameobjectImmunised = col.gameObject.transform.parent.gameObject;
			}
		}
	}
	
	void OnParticleCollision(GameObject go){
		if(go.gameObject.tag=="tete"){ // si le collider détecté est taggé "trait" ou "mur"
			ControlleurJoueur joueur = go.transform.parent.GetComponent<ControlleurJoueur>();
			if(go.transform.parent.gameObject != gameobjectImmunised){
				if(joueur.nbVie>1){
					joueur.enlever1Vie();
				}
				else{
					joueur.detruire();
				}
			}
		}
		else if (go.gameObject.tag=="badBonus" || go.gameObject.tag=="goodBonus"){
			Destroy(go.gameObject);
		}
	}

	public void lanceAnimation(){
		canBeDestroy = true;
		GetComponent<MeshRenderer>().enabled=false;
		GetComponent<SphereCollider>().enabled = false;
		GetComponent<ParticleSystem>().Play();
	}
}
