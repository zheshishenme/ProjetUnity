using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fuite : MonoBehaviour {

	//bloquer au limite du terrain !!

	List<GameObject> playersToEscape = new List<GameObject>();
	List<GameObject> bonusToEscape = new List<GameObject>();
	float speed = 0.4f;
	float count = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playersToEscape.Count>0){
			transform.position = Vector3.MoveTowards(transform.position,getDestination(getClosestPosition(ref playersToEscape)),  speed);
		}
		else if(bonusToEscape.Count>0){
			transform.position = Vector3.MoveTowards(transform.position,getDestination(getClosestPosition(ref bonusToEscape)),  speed);
		}
	}

	Vector3 getDestination(Vector3 objectPosition){
		float distx = Mathf.Abs(transform.position.x - objectPosition.x);
		float distz = Mathf.Abs(transform.position.z - objectPosition.z);

		distx= distx/(Mathf.Sqrt((distx*distx)+(distz*distz)));
		distz= distz/(Mathf.Sqrt((distx*distx)+(distz*distz)));

		float destx;
		float destz;

		if(objectPosition.x <= transform.position.x){
			destx = transform.position.x + distx;

		}
		else{
			destx = transform.position.x - distx;
		}

		if(objectPosition.z <= transform.position.z){
			destz = transform.position.z + distz;
		}
		else{
			destz = transform.position.z - distz;
		}
		//return new Vector3(destx,objectPosition.y,destz); // va sur les 3 niveaux
		return new Vector3(destx,transform.position.y,destz); // que sur son niveau
	}

	void OnTriggerEnter(Collider objet){
		if(objet.gameObject.tag=="tete"){
			playersToEscape.Add(objet.gameObject);
		}

		else if(objet.gameObject.tag=="badBonus" && objet != gameObject){
			bonusToEscape.Add(objet.gameObject);
		}

	}
	
	void OnTriggerExit(Collider objet){
		if(objet.gameObject.tag=="tete"){
			playersToEscape.Remove(objet.gameObject);
		}
		else if(objet.gameObject.tag=="badBonus"){
			bonusToEscape.Remove(objet.gameObject);
		}
	}
	
	Vector3 getClosestPosition(ref List<GameObject> listObject){
		GameObject closest = listObject[0];
		if(listObject.Count==1){
			if(closest!=null){ // permet d'éviter de le retourner si le jouerur meurt entre temps
				return closest.transform.position;
			}
			else{
				listObject.Remove(closest);
			}
			return Vector3.zero;
		}
		else{
			if(closest!=null){
				float distanceMin = Vector3.Distance(transform.position,closest.transform.position);
				for (int i = 1; i < listObject.Count; i++){
					if(listObject[i]!=null){
						float distance = Vector3.Distance(transform.position, listObject[i].transform.position);
						if(distance <= distanceMin){
							distanceMin=distance;
							closest = listObject[i];
						}
					}
					else{
						listObject.Remove(listObject[i]);
					}
				}
				return closest.transform.position;
			}
		}
		return Vector3.zero;
	}
}
