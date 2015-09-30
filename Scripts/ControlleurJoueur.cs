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
		//scriptMouvement = gameObject.GetComponent<mouvement>();
	}

	void Update(){
		if(ControlleurJeu.Instance.gameStarted){
			if(gameObject.name =="Joueur" ||gameObject.name =="ThirdPersonController" ){
				gameObject.transform.Find("tete").GetComponent<mouvement>().trait = Resources.Load("traitRouge") as GameObject;
				if(Input.GetKey(KeyCode.RightArrow) && ControlleurJeu.Instance.recordInput){
					//scriptMouvement.avance= false;
					scriptMouvement.droite();
				}
				
				else if(Input.GetKey(KeyCode.LeftArrow) && ControlleurJeu.Instance.recordInput){
					//scriptMouvement.avance= false;
					scriptMouvement.gauche();
				}
				else if(Input.GetKey(KeyCode.UpArrow) && ControlleurJeu.Instance.recordInput && scriptMouvement.peuxMonter()){
					ControlleurJeu.Instance.recordInput = false;
					//scriptMouvement.avance= false;
					scriptMouvement.avance= true;
					scriptMouvement.monte = true;
					scriptMouvement.haut();
				}
				else if(Input.GetKey(KeyCode.DownArrow) && ControlleurJeu.Instance.recordInput && scriptMouvement.peuxDescendre()){
					ControlleurJeu.Instance.recordInput = false;
					//scriptMouvement.avance= false;
					scriptMouvement.avance= true;
					scriptMouvement.descend = true;
					scriptMouvement.bas();
				}
				else{
					scriptMouvement.avance= true;
				}
				
			}

			else if(gameObject.name == "Joueur2"){
				gameObject.transform.Find("tete").GetComponent<mouvement>().trait = Resources.Load("traitBleu") as GameObject;
				if(Input.GetKey(KeyCode.E)&&ControlleurJeu.Instance.recordInput){
					scriptMouvement.droite();
				}
				
				if(Input.GetKey(KeyCode.A)&& ControlleurJeu.Instance.recordInput){
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
