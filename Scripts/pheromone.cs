using UnityEngine;
using System.Collections;

public class pheromone : MonoBehaviour {

	public deplacementFourmi myFourmiScript;

	float timer = 0;
	float dureeVie = 10;
	public float monRang;

	public ControllerFourmis.Type myType;


	// Use this for initialization
	void Start () {
		timer = 0;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(myType == ControllerFourmis.Type.Victoire){
			transform.localScale = new Vector3(transform.localScale.x, 30, transform.localScale.z);
		}

		else if(myType == ControllerFourmis.Type.Excitee){
			transform.localScale = new Vector3(transform.localScale.x, 15, transform.localScale.z);
		}

		if(timer >= dureeVie){
			if(myType == ControllerFourmis.Type.Victoire){
				ControllerFourmis.instance.deletePheromonesExcitees(gameObject);
			}

			if(myFourmiScript != null){
				myFourmiScript.removePheromone(gameObject);
			}

			Destroy(gameObject);
		}
	}
}
