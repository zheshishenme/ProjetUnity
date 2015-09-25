using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fuite : MonoBehaviour {

	//bloquer au limite du terrain !!

	List<GameObject> playersToEscape = new List<GameObject>();
	float speed = 0.4f;
	float count = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playersToEscape.Count>0){
			transform.position = Vector3.MoveTowards(transform.position,getDestination(getClosestPlayerPosition()),  speed);
		}
	}
	Vector3 getDestination(Vector3 playerPosition){
		float destX = transform.position.x;
		float destZ = transform.position.z;

		if(playerPosition.x <= destX){
			destX ++;
		}
		else{
			destX --;
		}

		if(playerPosition.z <= destZ){
			destZ ++;
		}
		else{
			destZ--;
		}
		return new Vector3(destX,playerPosition.y,destZ);
	}

	void OnTriggerEnter(Collider player){
		if(player.gameObject.tag=="tete"){
			playersToEscape.Add(player.gameObject);
		}
	}
	
	void OnTriggerExit(Collider player){
		if(player.gameObject.tag=="tete"){
			playersToEscape.Remove(player.gameObject);
		}
	}
	
	Vector3 getClosestPlayerPosition(){
		GameObject closest = playersToEscape[0];
		if(playersToEscape.Count==1){
			if(closest!=null){ // permet d'éviter de le retourner si le jouerur meurt entre temps
				return closest.transform.position;
			}
			else{
				playersToEscape.Remove(closest);
			}
			return Vector3.zero;
		}
		else{
			float distanceMin = Vector3.Distance(transform.position,closest.transform.position);
			for (int i = 1; i < playersToEscape.Count; i++){
				if(playersToEscape[i]!=null){
					float distance = Vector3.Distance(transform.position, playersToEscape[i].transform.position);
					if(distance <= distanceMin){
						distanceMin=distance;
						closest = playersToEscape[i];
					}
				}
				else{
					playersToEscape.Remove(playersToEscape[i]);
				}
			}
			return closest.transform.position;
		}
		return Vector3.zero;
	}
}
