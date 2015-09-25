using UnityEngine;
using System.Collections;

public class effetDark : MonoBehaviour {

	float timer = -10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(timer!=-10){
			timer-= Time.deltaTime;
			if(timer<=0){
				allumeLumiereAmbiante();
				eteindreSpotJoueur();
				timer = -10;
				ControllerBonus.Instance.bonusDarkHere=false;
				Destroy(gameObject);
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "tete"){
			eteindreLumiereAmbiante();
			allumerSpotJoueur();
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			foreach(Collider c in gameObject.GetComponents<Collider>()){
				c.enabled = false;
			}

			timer = 8;
		}
	}


	/// <summary>
	/// Eteindres the lumiere.
	/// </summary>
	void eteindreLumiereAmbiante(){
		GameObject.Find("sun").GetComponent<Light>().enabled =false;
	}

	/// <summary>
	/// Allume the lumiere.
	/// </summary>
	void allumeLumiereAmbiante(){
 		GameObject.Find("sun").GetComponent<Light>().enabled = true;
	}


	/// <summary>
	/// Allumers the spot joueur.
	/// </summary>
	void allumerSpotJoueur(){
		foreach(GameObject player in GameObject.FindGameObjectsWithTag("joueur")){
			player.transform.FindChild("tete").GetComponent<Light>().enabled = true;
		}
	}

		
	/// <summary>
	/// Allumers the spot joueur.
	/// </summary>
	void eteindreSpotJoueur(){
		foreach(GameObject player in GameObject.FindGameObjectsWithTag("joueur")){
			player.transform.FindChild("tete").GetComponent<Light>().enabled=false;
		}
	}
}
