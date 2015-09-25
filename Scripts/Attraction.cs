using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attraction : MonoBehaviour {

	List<GameObject> playersToFollow = new List<GameObject>();
	float speed = 0.9f;
	float count = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(playersToFollow.Count>0){
			transform.position = Vector3.MoveTowards(transform.position, getClosestPlayerPosition(), speed);
		}
	}

	void OnTriggerEnter(Collider player){
		if(player.gameObject.tag=="tete"){
			playersToFollow.Add(player.gameObject);
		}
	}

	void OnTriggerExit(Collider player){
		if(player.gameObject.tag=="tete"){
			playersToFollow.Remove(player.gameObject);
		}
	}

	Vector3 getClosestPlayerPosition(){
		GameObject closest = playersToFollow[0];
		if(playersToFollow.Count==1){
			if(closest!=null){ // permet d'éviter de le retourner si le jouerur meurt entre temps
				return closest.transform.position;
			}
			else{
				playersToFollow.Remove(closest);
			}
			return Vector3.zero;
		}
		else{
			float distanceMin = Vector3.Distance(transform.position,closest.transform.position);
			for (int i = 1; i < playersToFollow.Count; i++){
				if(playersToFollow[i]!=null){
					float distance = Vector3.Distance(transform.position, playersToFollow[i].transform.position);
					if(distance <= distanceMin){
						distanceMin=distance;
						closest = playersToFollow[i];
					}
				}
				else{
					playersToFollow.Remove(playersToFollow[i]);
				}
			}
			return closest.transform.position;
		}
		return Vector3.zero;
	}
}
