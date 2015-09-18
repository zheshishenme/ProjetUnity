using UnityEngine;
using System.Collections;

public class explode : MonoBehaviour {

	public GameObject explosion;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		explose();
		Destroy(gameObject);
	}

	void explose(){
		Instantiate(explosion, transform.position, transform.rotation);
	}
}
