using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attraction : MonoBehaviour {

	List<GameObject> playersToFollow = new List<GameObject>();
	List<GameObject> bonusToFollow = new List<GameObject>();
	float speed = 0.9f;
	float count = 0;

	public bool saute = false;
	public bool? monte = null;

	bool descend;

	Vector3 trajectoire;

	// Update is called once per frame
	void Update () {
		if(playersToFollow.Count>0){
			if (saute){
				if(monte==true){
					trajectoire = Vector3.MoveTowards(transform.position, getClosestPosition(ref playersToFollow) + new Vector3(0,10,0), speed);
					if(transform.position.y >= 23+23){
						monte = false;
					}
				}
				else if(monte == false){
					trajectoire = Vector3.MoveTowards(transform.position, getClosestPosition(ref playersToFollow) - new Vector3(0,10,0), speed);
					if(transform.position.y <= 23){
						monte = null;
						saute = false;
						transform.position = new Vector3(transform.position.x, 23, transform.position.z);
					}
				}

			}
			else{

				trajectoire = Vector3.MoveTowards(transform.position, getClosestPosition(ref playersToFollow), speed);
			}
			transform.position = trajectoire ;
		}


		else if(bonusToFollow.Count>0){
			if (saute){
				if(monte==true){
					trajectoire = Vector3.MoveTowards(transform.position, getClosestPosition(ref bonusToFollow) + new Vector3(0,10,0), speed);
					if(transform.position.y >= 23+23){
						monte = false;
					}
				}
				else if(monte==false){
					trajectoire = Vector3.MoveTowards(transform.position, getClosestPosition(ref bonusToFollow) - new Vector3(0,10,0), speed);
					if(transform.position.y <= 23){
						monte = null;
						saute = false;
						transform.position = new Vector3(transform.position.x, 23, transform.position.z);
					}
				}
				
			}
			else{
				trajectoire= Vector3.MoveTowards(transform.position, getClosestPosition(ref bonusToFollow), speed);
			}
			transform.position = trajectoire ;
		}
	}

	void OnTriggerEnter(Collider objet){
		if(objet.gameObject.tag=="tete"){
			playersToFollow.Add(objet.gameObject);
		}
		else if(objet.gameObject.tag=="goodBonus" && objet != gameObject){

			bonusToFollow.Add(objet.gameObject);
		}
	}

	void OnTriggerExit(Collider player){
		if(player.gameObject.tag=="tete"){
			playersToFollow.Remove(player.gameObject);
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
					if(listObject[i]!= null){
						float distance = Vector3.Distance(transform.position, listObject[i].transform.position);
						if(distance <= distanceMin){
							distanceMin=distance;
							closest = listObject[i];
						}
					}
					else{
						listObject.Remove(listObject[i]);
						return transform.position;
					}
				}
				return closest.transform.position;
			}
		}
		return transform.position;
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "goodBonus"){
			bonusToFollow= new List<GameObject>();
			if(col.gameObject.name == "bonusExplosionCercle"){
				col.gameObject.GetComponent<bonusExplosion>().lanceAnimation();
			}
			else{
				Destroy(col.gameObject);
			}
		}
	}

}
