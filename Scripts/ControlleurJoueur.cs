using UnityEngine;
using System.Collections;

public class ControlleurJoueur : MonoBehaviour{
	
	#region singleton
	private static ControlleurJoueur instance;
	
	// Static singleton property
	public static ControlleurJoueur Instance
	{
		// Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
		// otherwise we assign instance to a new component and return that
		get { 
			return instance ?? (instance = new GameObject("Singleton Controleur joueur").AddComponent<ControlleurJoueur>()); 
		}
	}
	#endregion

	mouvement scriptMouvement;


	void Start () {
		scriptMouvement = gameObject.transform.Find("tete").GetComponent<mouvement>();
	}

	void Update(){
		if(ControlleurJeu.Instance.gameStarted){
			if(gameObject.name =="Joueur"){
				gameObject.transform.Find("tete").GetComponent<mouvement>().trait = Resources.Load("traitRouge") as GameObject;
				if(Input.GetKey(KeyCode.RightArrow)){

					scriptMouvement.droite();
				}
				
				if(Input.GetKey(KeyCode.LeftArrow)){
					scriptMouvement.gauche();
				}
				
			}

			else if(gameObject.name == "Joueur2"){
				gameObject.transform.Find("tete").GetComponent<mouvement>().trait = Resources.Load("traitBleu") as GameObject;
				if(Input.GetKey(KeyCode.E)){
					scriptMouvement.droite();
				}
				
				if(Input.GetKey(KeyCode.A)){
					scriptMouvement.gauche();
				}
				
			}
		}
	}
	/// <summary>
	/// Autodestruction 
	/// </summary>
	public void detruire(){
		Destroy(gameObject);
	}
}
