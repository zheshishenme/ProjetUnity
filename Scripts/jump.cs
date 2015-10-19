using UnityEngine;
using System.Collections;

public class jump : MonoBehaviour {

	RaycastHit hit;
	
	// Update is called once per frame
	void Update () {

		Vector3 fwd = transform.TransformDirection(Vector3.forward);

		if (Physics.Raycast(transform.position, fwd,out hit, 40)) {
			if(hit.transform.gameObject.tag == "trait"){
				GetComponent<Attraction>().saute = true; 
				GetComponent<Attraction>().monte = true;
			}
		}
	}
}
