using UnityEngine;
using System.Collections;

public class ControlleurJoueur : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.transform.Find("Main Camera").GetComponent<cam>().target = gameObject.transform.Find("tete").GetComponent<Transform>();
		//GameObject.Find("Main Camera").GetComponent<cam>().target = GameObject.Find("tete").GetComponent<Transform>();
	}

	public void detruire(){
		Destroy(gameObject);
	}
}
