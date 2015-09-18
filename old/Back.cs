using UnityEngine;
using System.Collections;

public class Back : MonoBehaviour {

	public bool moove ;
	public float timer = 0f;

	public GameObject bille ;
	public GameObject pos;

	float hauteurTrait = 23f;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("up") ||(Input.GetKey("down")) || (Input.GetKey("left"))|| (Input.GetKey("right"))){
			moove = true;
		}
		else{
			moove = false;
		}

		if (moove){
			timer+= Time.deltaTime;

			if(timer>0.01){
				Instantiate(bille, sameY(pos.transform.position), pos.transform.rotation);
				timer = 0;
			}
		}
	}

	Vector3 sameY(Vector3 vectorToFreezeY){
		Vector3 result = new Vector3 (vectorToFreezeY.x,hauteurTrait,vectorToFreezeY.z);
		return result;
	}


}
