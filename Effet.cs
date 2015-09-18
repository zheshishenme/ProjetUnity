using UnityEngine;
using System.Collections;

public class Effet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.tag =="tete"){
			col.gameObject.GetComponent<mouvement>().ajouterSpeed(2f, 3f);
			Destroy (gameObject);
		}
	}
}
