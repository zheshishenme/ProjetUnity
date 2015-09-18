using UnityEngine;
using System.Collections;

public class shoot : MonoBehaviour {

	public Rigidbody projectile;
	public GameObject start;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.B)){
			Rigidbody clone;
			clone = Instantiate(projectile, start.transform.position , transform.rotation) as Rigidbody;
			clone.velocity = transform.TransformDirection(Vector3.forward * 200);
		}
	}
}
