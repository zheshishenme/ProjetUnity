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
		float distx = Mathf.Abs(transform.position.x - playerPosition.x);
		float distz = Mathf.Abs(transform.position.z - playerPosition.z);

		distx= distx/(Mathf.Sqrt((distx*distx)+(distz*distz)));
		distz= distz/(Mathf.Sqrt((distx*distx)+(distz*distz)));

		float destx;
		float destz;

		if(playerPosition.x <= transform.position.x){
			destx = transform.position.x + distx;

		}
		else{
			destx = transform.position.x - distx;
		}

		if(playerPosition.z <= transform.position.z){
			destz = transform.position.z + distz;
		}
		else{
			destz = transform.position.z - distz;
		}
		return new Vector3(destx,playerPosition.y,destz);
		/*
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
		}*/
		return Vector3.zero;
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
