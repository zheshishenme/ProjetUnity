using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {

	GameObject animeExplosion;

	void Start(){
		animeExplosion = Resources.Load("impact") as GameObject;//prend en mémoire l'objet d'explosion
	}

	/// <summary>
	/// Détection de collision
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionEnter(Collision col){ // dès qu'une collision est détectée
		if(col.gameObject.tag == "trait" || col.gameObject.tag == "mur"){ // si le collider détecté est taggé "trait" ou "mur"
			explosion();//fonction d'explosion
			ControlleurJeu.Instance.askNewGame();//lancement fonction du controlleur de jeu pour "recommencer"
			gameObject.transform.parent.GetComponent<ControlleurJoueur>().detruire();//on demande au Controlleur du joueur la focntion détruire
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
