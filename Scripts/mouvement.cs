using UnityEngine;
using System.Collections;

public class mouvement : MonoBehaviour {

	
	GameObject trait ;
	GameObject positionTrait;

	float hauteurTrait = 23f;
	float speed = 0.5f;
	float angleRotation = 2f;

	GameObject papa;

	void Start(){
		papa = transform.parent.gameObject;
		positionTrait = gameObject.transform.Find("departCorps").gameObject;
		trait = Resources.Load("traitRouge") as GameObject;
	}
	
	void Update () {

		if(ControlleurJeu.Instance.start){
			if(Input.GetKey(KeyCode.RightArrow)){
				droite();
			}

			/*if(Input.GetKey(KeyCode.UpArrow)){
				haut();
			}

			if(Input.GetKey(KeyCode.DownArrow)){
				Bas();
			}*/

			if(Input.GetKey(KeyCode.LeftArrow)){
				gauche();
			}

			avance();
			GameObject obj = Instantiate(trait, sameY(positionTrait.transform.position), positionTrait.transform.rotation) as GameObject;
			//GameObject obj = Instantiate(trait, positionTrait.transform.position, positionTrait.transform.rotation) as GameObject;

			//ajustement de la taille du corps en fonciton de la vitesse
			obj.transform.localScale = new Vector3( obj.transform.localScale.x, speed * 0.5f, obj.transform.localScale.z); //speed 1 => scale 0.5  |||  speed 2 => scale = 2*0.5


			obj.transform.parent = papa.transform;
			obj.name="Trait";
		}	
	}

	/// <summary>
	/// Modifie le vecteur d'entrée et le retourne avec le Y fixé à la valeur "hauteurTrait"
	/// </summary>
	/// <returns>The y.</returns>
	/// <param name="vectorToFreezeY">Vector to freeze y.</param>
	Vector3 sameY(Vector3 vectorToFreezeY){
		Vector3 result = new Vector3 (vectorToFreezeY.x,hauteurTrait,vectorToFreezeY.z);
		return result;
	}

	/// <summary>
	/// Fais avancer la tete
	/// </summary>
	void avance(){
		gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speed);
	}


	/// <summary>
	/// Tourne la tete de "angle rotation" sur la droite
	/// </summary>
	void droite(){
		transform.Rotate(0,angleRotation,0);
	}

	/// <summary>
	/// Tourne la tete de "angle rotation" sur la gauche
	/// </summary>
	void gauche(){
		transform.Rotate(0,-angleRotation,0);
	}

	/// <summary>
	/// Lève la tete de angle rotation
	/// </summary>
	void haut(){
		transform.Rotate(-angleRotation,0,0);
	}

	/// <summary>
	/// Baisse la tete de angle rotation
	/// </summary>
	void Bas(){
		transform.Rotate(-angleRotation,0,0);
	}
}
