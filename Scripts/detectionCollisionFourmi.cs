using UnityEngine;
using System.Collections;

public class detectionCollisionFourmi : MonoBehaviour {

	deplacementFourmi scriptMaFourmi;
	// Use this for initialization
	void Start () {
		scriptMaFourmi = gameObject.transform.parent.gameObject.GetComponent<deplacementFourmi>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider objet){

		if(objet.gameObject.tag=="mur"){
			scriptMaFourmi.stop = true;
			scriptMaFourmi.doitTourner = true;
			scriptMaFourmi.evitement = Random.Range(0,2);
		}
		else if(objet.gameObject.tag=="etoile"){
			scriptMaFourmi.vientDeTrouverEtoile(objet.gameObject);
		}
		else if(objet.gameObject.tag=="pheromone"){
			scriptMaFourmi.vientDeTrouverPheromone(objet.gameObject);
		}
	}

	void OnTriggerExit(Collider objet){
		if(objet.gameObject.tag =="mur"){
			scriptMaFourmi.stop = false;
			scriptMaFourmi.doitTourner = false;
		}

	}
	void OnTriggerStay(Collider objet){
		if(objet.gameObject.tag == "mur"){
			scriptMaFourmi.demiTour();
		}
	}
}
