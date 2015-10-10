using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {

	GameObject animeExplosion;
	float finTimer = 0.7f;
	float timerRecordCollision = -10 ;

	void Start(){
		animeExplosion = Resources.Load("impact") as GameObject;//prend en mémoire l'objet d'explosion
	}


	void Update(){
		if(timerRecordCollision >= 0){
			timerRecordCollision+=Time.deltaTime;
			if(timerRecordCollision>=finTimer){
				timerRecordCollision=-10;
				gameObject.GetComponent<CapsuleCollider>().enabled = true;
			}
		}
	}
	/// <summary>
	/// Détection de collision
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionEnter(Collision col){ // dès qu'une collision est détectée
		ControlleurJoueur joueur  = gameObject.transform.parent.GetComponent<ControlleurJoueur>();

		if(col.gameObject.tag == "trait" || col.gameObject.tag=="tete"){ // si le collider détecté est taggé "trait" ou "mur"
			gameObject.GetComponent<CapsuleCollider>().enabled = false;
			timerRecordCollision = 0;
			if(joueur.nbVie>1){
				joueur.enlever1Vie();
			}
			else{
				explosion();//fonction d'explosion
				ControlleurJeu.instance.askNewGame();//lancement fonction du controlleur de jeu pour "recommencer"
				joueur.detruire();//on demande au Controlleur du joueur la focntion détruire
			}
		}
		else if(col.gameObject.tag == "mur" ){
			explosion();//fonction d'explosion
			ControlleurJeu.instance.askNewGame();//lancement fonction du controlleur de jeu pour "recommencer"
			joueur.detruire();//on demande au Controlleur du joueur la focntion détruire
		}
	}

	/// <summary>
	/// Fonction d'explosion, va instancier les particules d'explosions
	/// </summary>
	void explosion(){
		GameObject go = Instantiate(animeExplosion, transform.position,transform.rotation) as GameObject;
		go.name = "Explosion";
	}
}
