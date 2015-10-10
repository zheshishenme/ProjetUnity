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
	public bool doitMonter = false;
	public bool doitDescendre = false;

	public bool canGoUp = true;
	public bool canGoDown = true;

	public bool doitSauter;

	//float lastStage;

	GameObject corps;

	void Start(){
		corps = transform.parent.gameObject.transform.Find("corps").gameObject;
		positionTrait = gameObject.transform.Find("departCorps").gameObject;
	}
	
	void Update () {
		if(ControlleurJeu.instance.gameStarted){	
			if(avance){
				avancer();
			}
			if(doitSauter){
				if(doitMonter){
					if(transform.position.y >= hauteurTrait + hauteurTrait/2){
						doitMonter = false;
						transform.Rotate(transform.rotation.x,0,0);
						orienteDescente ();
						orienteDescente ();
						doitDescendre=true;
					}
				}
				if(doitDescendre){
					if(transform.position.y <= hauteurTrait){
						doitDescendre = false;
						transform.Rotate(-transform.rotation.x,0,0);
						orienteMontee ();
						ControlleurJeu.instance.recordInput = true;
						doitSauter = false;
					}
				}
			}

			if(traceTrait){
				timeTrace+=Time.deltaTime;
				if(timeTrace >= 0.2f/speed){
					trace();
					timeTrace=0;
				}
			}
		}
	}

	/*public bool peuxMonter(){
		if(transform.position.y >= 25){ // >= hauteurtrait + hauteurtrait/2 -6 pour etre sur
			return false;
		}
		return true;
	}

	public bool peuxDescendre(){
		if(transform.position.y <= 21 ){ // >= hauteurtrait - hauteurtrait/2 +6 pour etre sur
			return false;
		}
		return true;
	}*/

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
	public void tourneDroite(){
		/*myAnimation.SetBool("left",false);
		myAnimation.SetBool("right",true);*/
		transform.Rotate(0,angleRotation,0);
	}

	/// <summary>
	/// Tourne la tete de "angle rotation" sur la gauche
	/// </summary>
	public void tourneGauche(){
		/*myAnimation.SetBool("left",true);
		myAnimation.SetBool("right",false);*/
		transform.Rotate(0,-angleRotation,0);
	}

	/// <summary>
	/// Lève la tete de angle rotation
	/// </summary>
	public void orienteMontee(){
		//lastStage = transform.position.y;
		transform.Rotate(-10* angleRotation,0,0);
	}

	/// <summary>
	/// Baisse la tete de angle rotation
	/// </summary>
	public void orienteDescente(){
		//lastStage = transform.position.y;
		transform.Rotate(10* angleRotation,0,0);
	}

	#endregion

	#region boosters

	public void ajouterSpeed(float speedToAdd){
		speed += speedToAdd;
	}

	#endregion
}
