using UnityEngine;
using System.Collections;

public class mouvement : MonoBehaviour {

	
	public GameObject trait ;
	GameObject positionTrait;

	float hauteurTrait = 23f;
	public float speed = 1f;
	float speedAdded;
	float angleRotation = 2f;
	public bool traceTrait = true; // permet de controler la trace du joueur;

	float timeTrace=0;

	GameObject corps;

	void Start(){
		corps = transform.parent.gameObject.transform.Find("corps").gameObject;
		positionTrait = gameObject.transform.Find("departCorps").gameObject;
	}
	
	void Update () {
		if(ControlleurJeu.Instance.gameStarted){		
			avance();

			if(traceTrait){
				timeTrace+=Time.deltaTime;
				if(timeTrace >= 0.1f/speed){
					trace();
					timeTrace=0;
				}
			}
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
	/// Permet de tracer le corps du joueur
	/// </summary>
	public void trace(){
		GameObject obj = Instantiate(trait, sameY(positionTrait.transform.position), positionTrait.transform.rotation) as GameObject;
		obj.transform.parent = corps.transform;
		obj.name="Trait";
	}

	#region mouvements basiques
	
	/// <summary>
	/// Fais avancer la tete
	/// </summary>
	void avance(){
		gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speed);
	}
	
	/// <summary>
	/// Tourne la tete de "angle rotation" sur la droite
	/// </summary>
	public void droite(){
		transform.Rotate(0,angleRotation,0);
	}

	/// <summary>
	/// Tourne la tete de "angle rotation" sur la gauche
	/// </summary>
	public void gauche(){
		transform.Rotate(0,-angleRotation,0);
	}

	/// <summary>
	/// Lève la tete de angle rotation
	/// </summary>
	public void haut(){
		transform.Rotate(-angleRotation,0,0);
	}

	/// <summary>
	/// Baisse la tete de angle rotation
	/// </summary>
	public void Bas(){
		transform.Rotate(-angleRotation,0,0);
	}

	#endregion

	#region boosters

	public void ajouterSpeed(float speedToAdd){
		speed += speedToAdd;
	}

	#endregion
}
