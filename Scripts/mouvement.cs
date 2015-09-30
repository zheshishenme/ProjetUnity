using UnityEngine;
using System.Collections;

public class mouvement : MonoBehaviour {

	
	public GameObject trait ;
	GameObject positionTrait;
	public Animator myAnimation;
	float hauteurTrait = 23f;
	public float speed = 1;
	float speedAdded;
	float angleRotation = 2f;
	public bool traceTrait = true; // permet de controler la trace du joueur;

	public bool avance = true;

	float timeTrace=0;
	public bool monte = false;
	public bool descend = false;

	public bool canGoUp = true;
	public bool canGoDown = true;

	float lastStage;

	GameObject corps;

	void Start(){
		corps = transform.parent.gameObject.transform.Find("corps").gameObject;
		positionTrait = gameObject.transform.Find("departCorps").gameObject;
	}
	
	void Update () {
		if(ControlleurJeu.Instance.gameStarted){	
			if(avance){
				avancer();
			}

			if(monte){
				if(transform.position.y >= lastStage + hauteurTrait/2){
					monte = false;
					transform.Rotate(transform.rotation.x,0,0);
					bas ();
					ControlleurJeu.Instance.recordInput = true;
				}
			}
			if(descend){
				if(transform.position.y <= lastStage - hauteurTrait/2){
					descend = false;
					transform.Rotate(-transform.rotation.x,0,0);
					haut ();
					ControlleurJeu.Instance.recordInput = true;
				}
			}


			if(traceTrait){
				timeTrace+=Time.deltaTime;
				if(timeTrace >= 0.1f/speed){
					trace();
					timeTrace=0;
				}
			}
		}
		else{

			//myAnimation.SetBool("run",false);

		}
	}

	public bool peuxMonter(){
		if(transform.position.y >= 30){ // >= hauteurtrait + hauteurtrait/2 -6 pour etre sur
			return false;
		}
		return true;
	}

	public bool peuxDescendre(){
		if(transform.position.y <= 19 ){ // >= hauteurtrait - hauteurtrait/2 +6 pour etre sur
			return false;
		}
		return true;
	}

	/*
	/// <summary>
	/// Modifie le vecteur d'entrée et le retourne avec le Y fixé à la valeur "hauteurTrait"
	/// </summary>
	/// <returns>The y.</returns>
	/// <param name="vectorToFreezeY">Vector to freeze y.</param>
	Vector3 sameY(Vector3 vectorToFreezeY){
		Vector3 result = new Vector3 (vectorToFreezeY.x,hauteurTrait,vectorToFreezeY.z);
		return result;
	}
	 */

	/// <summary>
	/// Permet de tracer le corps du joueur
	/// </summary>
	public void trace(){
		//GameObject obj = Instantiate(trait, sameY(positionTrait.transform.position), positionTrait.transform.rotation) as GameObject;
		GameObject obj = Instantiate(trait, positionTrait.transform.position, positionTrait.transform.rotation) as GameObject;
		obj.transform.parent = corps.transform;
		obj.name="Trait";
	}

	#region mouvements basiques
	
	/// <summary>
	/// Fais avancer la tete
	/// </summary>
	void avancer(){	
/*		myAnimation.SetBool("left",false);
		myAnimation.SetBool("right",false);

		myAnimation.SetBool("run",true);*/

		gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speed);
	}
	
	/// <summary>
	/// Tourne la tete de "angle rotation" sur la droite
	/// </summary>
	public void droite(){
		/*myAnimation.SetBool("left",false);
		myAnimation.SetBool("right",true);*/
		transform.Rotate(0,angleRotation,0);
	}

	/// <summary>
	/// Tourne la tete de "angle rotation" sur la gauche
	/// </summary>
	public void gauche(){
		/*myAnimation.SetBool("left",true);
		myAnimation.SetBool("right",false);*/
		transform.Rotate(0,-angleRotation,0);
	}

	/// <summary>
	/// Lève la tete de angle rotation
	/// </summary>
	public void haut(){
		lastStage = transform.position.y;
		transform.Rotate(-10* angleRotation,0,0);
	}

	/// <summary>
	/// Baisse la tete de angle rotation
	/// </summary>
	public void bas(){
		lastStage = transform.position.y;
		transform.Rotate(10* angleRotation,0,0);
	}

	#endregion

	#region boosters

	public void ajouterSpeed(float speedToAdd){
		speed += speedToAdd;
	}

	#endregion
}
