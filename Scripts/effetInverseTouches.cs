using UnityEngine;
using System.Collections;

public class effetInverseTouches : MonoBehaviour {

	float timer = -1;
	float inverseTime = 6;

	// Update is called once per frame
	void Update () {
		if(timer >=0){
			timer+=Time.deltaTime;
			if(timer >= inverseTime){
				ControlleurJeu.Instance.touchesInversees = false;
				Destroy (gameObject);
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag=="tete"){
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<SphereCollider>().enabled = false;
			timer = 0;
			ControlleurJeu.Instance.touchesInversees = true;
		}
	}

}
