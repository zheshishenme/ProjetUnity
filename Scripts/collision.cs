using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {

	GameObject animeExplosion;

	void Start(){
		animeExplosion = Resources.Load("impact") as GameObject;
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "trait" || col.gameObject.tag == "mur"){
			explosion();
			ControlleurJeu.Instance.askNewGame();
			gameObject.transform.parent.GetComponent<ControlleurJoueur>().detruire();;
		}
	}

	void explosion(){
		GameObject go = Instantiate(animeExplosion, transform.position,transform.rotation) as GameObject;
		go.name = "Explosion";
	}
}
