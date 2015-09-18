using UnityEngine;
using System.Collections;

public class mouvement : MonoBehaviour {

	
	GameObject trait ;
	GameObject positionTrait;

	float hauteurTrait = 23f;
	float speed = 1f;
	GameObject papa;

	void Start(){
		papa = transform.parent.gameObject;
		positionTrait = gameObject.transform.Find("departCorps").gameObject;
		trait = Resources.Load("traitRouge") as GameObject;
	}

	// Update is called once per frame
	void Update () {

		if(ControlleurJeu.Instance.start){
			if(Input.GetKey(KeyCode.RightArrow)){
				droite();
			}

			if(Input.GetKey(KeyCode.UpArrow)){
				haut();
			}

			if(Input.GetKey(KeyCode.LeftArrow)){
				gauche();
			}

			avance();
			//GameObject obj = Instantiate(trait, sameY(positionTrait.transform.position), positionTrait.transform.rotation)as GameObject;
			GameObject obj = Instantiate(trait, positionTrait.transform.position, positionTrait.transform.rotation) as GameObject;
			obj.transform.parent = papa.transform;
			obj.name="Trait";
		}	
	}

	Vector3 sameY(Vector3 vectorToFreezeY){
		Vector3 result = new Vector3 (vectorToFreezeY.x,hauteurTrait,vectorToFreezeY.z);
		return result;
	}
	
	void avance(){
		gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speed);
	}

	void droite(){
		transform.Rotate(0,2,0);
	}

	void gauche(){
		transform.Rotate(0,-2,0);
	}

	void haut(){
		positionTrait.transform.Rotate(-2,0,0);
		transform.Rotate(-2,0,0);
	}
}
